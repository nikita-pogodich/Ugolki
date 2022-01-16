using System;
using System.Collections.Generic;

namespace UgolkiController
{
    public class UgolkiController : IUgolkiController
    {
        private const int _boardSize = 8;

        private IUgolkiExternalView _ugolkiExternalView;
        private List<string> _rules;
        private string _currentRule;
        private BoardCellType[,] _board = new BoardCellType[_boardSize, _boardSize];
        private Dictionary<Player, int> _movesInfo = new Dictionary<Player, int>();
        private Player _currentPlayer;
        private Coord _selectedPiecePosition;
        private bool _hasSelectedPiece;

        public UgolkiController(IUgolkiExternalView ugolkiExternalView)
        {
            _ugolkiExternalView = ugolkiExternalView;

            _rules = new List<string>
            {
                UgolkiRules.Rule1,
                UgolkiRules.Rule2,
                UgolkiRules.Rule3
            };
        }

        public event Action<Dictionary<Player, int>> MoveInfoChanged;

        public List<string> GetRules()
        {
            return _rules;
        }

        public void SetRule(string rule)
        {
            _currentRule = rule;
        }

        public void StartGame()
        {
            ResetBoard();

            _movesInfo.Clear();
            _movesInfo.Add(Player.White, 0);
            _movesInfo.Add(Player.Black, 0);

            OnMoveInfoChanged(_movesInfo);

            _currentPlayer = Player.White;
            _ugolkiExternalView.StartGame(_board, _boardSize);
        }

        public void EndGame()
        {
            ResetBoard();
            _ugolkiExternalView.EndGame(_board);
        }

        public Player CheckWinner()
        {
            throw new System.NotImplementedException();
        }

        public void TrySelectPiece(Coord cell)
        {
            if (_hasSelectedPiece == true && _selectedPiecePosition == cell)
            {
                _hasSelectedPiece = false;
                _ugolkiExternalView.DeselectPiece(cell);
                return;
            }

            if (_board[cell.Row, cell.Column] == BoardCellType.Empty)
            {
                _ugolkiExternalView.ShowMessage("select_piece");
                return;
            }

            if (_board[cell.Row, cell.Column] == BoardCellType.White && _currentPlayer == Player.White ||
                _board[cell.Row, cell.Column] == BoardCellType.Black && _currentPlayer == Player.Black)
            {
                _hasSelectedPiece = true;
                _selectedPiecePosition = cell;

                List<Coord> availableMoves = GetAvailableMoves(cell);
                _ugolkiExternalView.SelectPiece(cell, availableMoves);
            }
            else
            {
                _ugolkiExternalView.ShowMessage("not_your_move");
            }
        }

        public void TryMovePiece(Coord from, Coord to, Player player)
        {
            throw new System.NotImplementedException();
        }

        public List<Coord> GetAvailableMoves(Coord from)
        {
            Queue<Coord> toCheck = new Queue<Coord>();
            List<Coord> canJump = new List<Coord>();
            canJump.Add(from);
            toCheck.Enqueue(from);
            Coord currentFrom;
            Coord currentTo;
            Coord currentTo2;

            while (toCheck.Count > 0)
            {
                currentFrom = toCheck.Dequeue();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        try
                        {
                            currentTo = new Coord(currentFrom.Row + i, currentFrom.Column + j);
                            currentTo2 = new Coord(currentFrom.Row + i * 2, currentFrom.Column + j * 2);
                        }
                        catch (OutOfBoardException)
                        {
                            continue;
                        }

                        if (_board[currentTo.Row, currentTo.Column] != BoardCellType.Empty &&
                            _board[currentTo2.Row, currentTo2.Column] == BoardCellType.Empty &&
                            canJump.Contains(currentTo2) == false)
                        {
                            toCheck.Enqueue(currentTo2);
                            canJump.Add(currentTo2);
                        }
                    }
                }
            }

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    try
                    {
                        currentTo = new Coord(from.Row + i, from.Column + j);
                    }
                    catch (OutOfBoardException)
                    {
                        continue;
                    }

                    if (_board[currentTo.Row, currentTo.Column] == BoardCellType.Empty &&
                        canJump.Contains(currentTo) == false)
                    {
                        canJump.Add(currentTo);
                    }
                }
            }

            canJump.Remove(from);
            return canJump;
        }

        private void OnMoveInfoChanged(Dictionary<Player, int> movesInfo)
        {
            MoveInfoChanged?.Invoke(movesInfo);
        }

        private void ResetBoard()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (i <= 2 && j <= 2)
                    {
                        _board[i, j] = BoardCellType.White;
                    }
                    else if (i >= 5 && j >= 5)
                    {
                        _board[i, j] = BoardCellType.Black;
                    }
                    else
                    {
                        _board[i, j] = BoardCellType.Empty;
                    }
                }
            }
        }
    }
}