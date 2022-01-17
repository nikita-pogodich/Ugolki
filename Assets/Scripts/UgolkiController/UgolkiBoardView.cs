using System;
using System.Collections.Generic;
using Core.Managers.Logger;
using Core.Managers.PoolingManager;
using Settings;
using UnityEngine;

namespace UgolkiController
{
    public class UgolkiBoardView : MonoBehaviour, IUgolkiExternalView
    {
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

        private IPoolingManager _poolingManager;
        private IUgolkiController _ugolkiController;
        private List<List<GameObject>> _board = new List<List<GameObject>>();
        private int _boardSize;
        private bool _isGameStarted;

        public void Initialize(IPoolingManager poolingManager, IUgolkiController ugolkiController)
        {
            _poolingManager = poolingManager;
            _ugolkiController = ugolkiController;
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
            _isGameStarted = false;
            LogManager.LogDebug($"Winner: {winner.Value.ToString()}");
        }

        private void SetCellHighlightShown(bool isShown)
        {
            _cellHighlight.gameObject.SetActive(isShown);
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

        void IUgolkiExternalView.MovePiece(List<Move> path, Action onComplete)
        {
            Move move = path[0];
            GameObject piece = _board[move.From.Row][move.From.Column];
            piece.transform.localPosition = new Vector3(move.To.Row, 0.0f, move.To.Column);
            SetCellHighlightShown(false);

            _board[move.From.Row][move.From.Column] = null;
            _board[move.To.Row][move.To.Column] = piece;
            onComplete?.Invoke();
        }

        void IUgolkiExternalView.ShowMessage(string message)
        {
            LogManager.LogDebug(message);
        }
    }
}