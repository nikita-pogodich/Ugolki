using System;
using System.Collections;
using System.Collections.Generic;
using Core.Managers.PoolingManager;
using Core.Managers.ViewManager;
using DG.Tweening;
using Settings;
using UnityEngine;
using ViewControllers.GameResultPopup;
using ViewControllers.MessagePopup;

namespace UgolkiController
{
    public class UgolkiBoardView : MonoBehaviour, IUgolkiExternalView
    {
        private const double _jumpMinDistance = 2.0;

        [SerializeField]
        private Transform _piecesRoot;

        [SerializeField]
        private Transform _cellHighlight;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private BoxCollider _boardCollider;

        [SerializeField]
        private float _cellSize;

        [SerializeField]
        private string _boardTag;

        [SerializeField]
        private Ease _pieceMoveEasing = Ease.OutExpo;

        [SerializeField]
        private float _pieceMoveDuration = 0.2f;

        [SerializeField]
        private float _jumpHeight = 1.0f;

        private IPoolingManager _poolingManager;
        private IUgolkiController _ugolkiController;
        private IViewManager _viewManager;
        private IMessagePopupModel _messagePopupModel;
        private IGameResultPopupModel _gameResultPopupModel;
        private List<List<GameObject>> _board = new List<List<GameObject>>();
        private int _boardSize;
        private bool _isGameStarted;

        private Sequence _animation;

        public void Initialize(
            IPoolingManager poolingManager,
            IUgolkiController ugolkiController,
            IViewManager viewManager,
            IMessagePopupModel messagePopupModel,
            IGameResultPopupModel gameResultPopupModel)
        {
            _poolingManager = poolingManager;
            _ugolkiController = ugolkiController;
            _viewManager = viewManager;
            _messagePopupModel = messagePopupModel;
            _gameResultPopupModel = gameResultPopupModel;
        }

        private void Update()
        {
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (_isGameStarted == false)
            {
                return;
            }

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) == true)
            {
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Transform objectHit = hit.transform;
                    if (objectHit.CompareTag(_boardTag) == true)
                    {
                        Vector3 position = SnapToGrid(hit.point);
                        Vector3 localPoint = _piecesRoot.InverseTransformPoint(position);
                        _ugolkiController.TrySelectCell(new Coord((int)localPoint.x, (int)localPoint.z));
                    }
                }
            }
        }

        private Vector3 SnapToGrid(Vector3 pos)
        {
            if (_boardSize == 0)
            {
                return Vector3.zero;
            }

            float gridSnap = _boardCollider.size.x / _boardSize;
            float cellCenter = gridSnap / 2.0f;

            Vector3 snapHits = new Vector3(
                Mathf.Round((pos.x - cellCenter) / gridSnap) * gridSnap + cellCenter,
                pos.y,
                Mathf.Round((pos.z - cellCenter) / gridSnap) * gridSnap + cellCenter);

            return snapHits;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < _board.Count; i++)
            {
                if (_board[i] == null)
                {
                    continue;
                }

                for (int j = 0; j < _board[i].Count; j++)
                {
                    if (_board[i][j] != null)
                    {
                        //TODO create PieceInfo with ResourceKey field class and store it _board
                        _poolingManager.ReleaseResource(_board[i][j].name, _board[i][j]);
                    }
                }

                _board[i].Clear();
            }

            _board.Clear();
        }

        private GameObject CreatePiece(int row, int column, BoardCellType cellType)
        {
            string resultResourceName;
            if (cellType == BoardCellType.White)
            {
                resultResourceName = ResourceNamesList.PieceWhite;
            }
            else
            {
                resultResourceName = ResourceNamesList.PieceBlack;
            }

            GameObject resource = _poolingManager.GetResource(resultResourceName);
            Transform piece = resource.transform;
            piece.SetParent(_piecesRoot);
            piece.localPosition = new Vector3(_cellSize * row, 0.0f, _cellSize * column);
            piece.localScale = Vector3.one;
            piece.localEulerAngles = Vector3.zero;

            return resource;
        }

        void IUgolkiExternalView.StartGame(BoardCellType[,] board, int boardSize)
        {
            _boardSize = boardSize;
            ClearBoard();

            SetCellHighlightShown(false);

            for (int i = 0; i < boardSize; i++)
            {
                _board.Add(new List<GameObject>());
                for (int j = 0; j < boardSize; j++)
                {
                    if (board[i, j] == BoardCellType.Empty)
                    {
                        _board[i].Add(null);
                    }
                    else if (board[i, j] == BoardCellType.White)
                    {
                        GameObject piece = CreatePiece(i, j, BoardCellType.White);
                        _board[i].Add(piece);
                    }
                    else
                    {
                        GameObject piece = CreatePiece(i, j, BoardCellType.Black);
                        _board[i].Add(piece);
                    }
                }
            }

            _isGameStarted = true;
        }

        public void ShowWinner(Player? winner)
        {
            if (winner == null)
            {
                return;
            }

            _isGameStarted = false;

            _gameResultPopupModel.UpdateModel(winner.Value, OnRestart, OnBackToMenu);
            _viewManager.ShowView(ViewNamesList.GameResultPopup);
        }

        private void OnRestart()
        {
            _ugolkiController.EndGame();
            _ugolkiController.StartGame();
        }

        private void OnBackToMenu()
        {
            _ugolkiController.EndGame();
            _viewManager.ShowView(ViewNamesList.MainMenu);
        }

        private void SetCellHighlightShown(bool isShown)
        {
            _cellHighlight.gameObject.SetActive(isShown);
        }

        private void OnPieceMoveComplete(List<Coord> moves, Action onComplete)
        {
            Coord sourceCell = moves[0];
            Coord destinationCell = moves[moves.Count - 1];

            GameObject piece = _board[sourceCell.Row][sourceCell.Column];

            _board[sourceCell.Row][sourceCell.Column] = null;
            _board[destinationCell.Row][destinationCell.Column] = piece;

            onComplete?.Invoke();
        }

        private void PieceMoveAnimation(List<Coord> moves, Action onComplete)
        {
            _animation?.Kill();
            _animation = DOTween.Sequence();

            Coord sourceCell = moves[0];
            GameObject piece = _board[sourceCell.Row][sourceCell.Column];

            for (int i = 1; i < moves.Count; i++)
            {
                float jumpHeight = 0.0f;
                int jumps = 0;

                int moveDirectionRow = Math.Abs(sourceCell.Row - moves[i].Row);
                int moveDirectionColumn = Math.Abs(sourceCell.Column - moves[i].Column);
                Coord moveDirection = new Coord(moveDirectionRow, moveDirectionColumn);

                double moveMagnitude = moveDirection.Magnitude();
                if (moveMagnitude >= _jumpMinDistance)
                {
                    jumpHeight = _jumpHeight;
                    jumps = 1;
                }

                Vector3 destinationPosition = new Vector3(moves[i].Row, 0.0f, moves[i].Column);
                Sequence pieceMove = piece.transform
                    .DOLocalJump(destinationPosition, jumpHeight, jumps, _pieceMoveDuration)
                    .SetEase(_pieceMoveEasing);

                _animation.Append(pieceMove).OnComplete(OnAnimationComplete);
            }

            void OnAnimationComplete()
            {
                OnPieceMoveComplete(moves, onComplete);
            }
        }

        void IUgolkiExternalView.EndGame(BoardCellType[,] board)
        {
            ClearBoard();
            _isGameStarted = false;
        }

        void IUgolkiExternalView.SelectPiece(Coord coord, List<Coord> availableMoves)
        {
            SetCellHighlightShown(true);
            _cellHighlight.localPosition = new Vector3(coord.Row, 0.0f, coord.Column);
        }

        void IUgolkiExternalView.DeselectPiece(Coord coord)
        {
            SetCellHighlightShown(false);
        }

        void IUgolkiExternalView.MovePiece(List<Coord> moves, Action onComplete)
        {
            SetCellHighlightShown(false);
            PieceMoveAnimation(moves, onComplete);
        }

        void IUgolkiExternalView.ShowMessage(string message)
        {
            _messagePopupModel.UpdateMessageKey(message);
            _viewManager.ShowView(ViewNamesList.MessagePopup);
        }
    }
}