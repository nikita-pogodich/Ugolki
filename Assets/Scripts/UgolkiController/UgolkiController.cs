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
            _ugolkiExternalView.StartGame(_board, _boardSize);

            _movesInfo.Clear();
            _movesInfo.Add(Player.White, 0);
            _movesInfo.Add(Player.Black, 0);

            OnMoveInfoChanged(_movesInfo);
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

        public bool TrySelectPiece(Coord cell, out string errorType)
        {
            throw new System.NotImplementedException();
        }

        public bool TryMovePiece(Coord @from, Coord to, Player player, string errorType)
        {
            throw new System.NotImplementedException();
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