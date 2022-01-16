using System.Collections.Generic;
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
        private float _cellSize;

        private IPoolingManager _poolingManager;

        private List<List<GameObject>> _board = new List<List<GameObject>>();

        public void Initialize(IPoolingManager poolingManager)
        {
            _poolingManager = poolingManager;
        }

        void IUgolkiExternalView.StartGame(BoardCellType[,] board, int boardSize)
        {
            ClearBoard();

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
        }

        void IUgolkiExternalView.EndGame(BoardCellType[,] board)
        {
            ClearBoard();
        }

        void IUgolkiExternalView.SelectPiece(Coord coord)
        { }

        void IUgolkiExternalView.DeselectPiece(Coord coord)
        { }

        void IUgolkiExternalView.MovePiece(List<Move> path)
        { }

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
            
            //TODO remove this when PieceInfo will be created
            resource.name = resource.name.Replace("(Clone)", "");

            Transform piece = resource.transform;
            piece.SetParent(_piecesRoot);
            piece.localPosition = new Vector3(_cellSize * row, 0.0f, _cellSize * column);
            piece.localScale = Vector3.one;
            piece.localEulerAngles = Vector3.zero;

            return resource;
        }
    }
}