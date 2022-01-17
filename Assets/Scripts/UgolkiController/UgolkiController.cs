using System;
using System.Collections.Generic;
using UgolkiController.UgolkiRules;

namespace UgolkiController
{
    public class UgolkiController : IUgolkiController
    {
        private const int _boardSize = 8;
        private const int _whiteHousePosition = 2;
        private const int _blackHousePosition = 5;

        private IUgolkiExternalView _ugolkiExternalView;
        private List<string> _rules;
        private string _currentRule;
        private BoardCellType[,] _board = new BoardCellType[_boardSize, _boardSize];
        private Dictionary<Player, int> _movesInfo = new Dictionary<Player, int>();
        private Dictionary<string, BaseUgolkiRule> _availableMovesByRule = new Dictionary<string, BaseUgolkiRule>();
        private Player _currentPlayer;
        private Coord _selectedPiecePosition;
        private bool _hasSelectedPiece;
        private List<Coord> _currentAvailableMoves = new List<Coord>();

        public event Action<Dictionary<Player, int>> MoveInfoChanged;
        public event Action<Player> PlayerChanged;

        private CannotJumpRule _cannotJumpRule = new CannotJumpRule();
        private CanJumpOrthogonallyRule _canJumpOrthogonallyRule = new CanJumpOrthogonallyRule();
        private CanJumpDiagonallyRule _canJumpDiagonallyRule = new CanJumpDiagonallyRule();

        public UgolkiController(IUgolkiExternalView ugolkiExternalView)
        {
            _ugolkiExternalView = ugolkiExternalView;

            _rules = new List<string>
            {
                UgolkiRulesList.Rule1,
                UgolkiRulesList.Rule2,
                UgolkiRulesList.Rule3
            };

            _availableMovesByRule.Add(UgolkiRulesList.Rule1, _canJumpDiagonallyRule);
            _availableMovesByRule.Add(UgolkiRulesList.Rule2, _canJumpOrthogonallyRule);
            _availableMovesByRule.Add(UgolkiRulesList.Rule3, _cannotJumpRule);
        }

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
            ResetMovesInfo();
            ResetPlayer();

            _ugolkiExternalView.StartGame(_board, _boardSize);
        }

        private void ResetPlayer()
        {
            _currentPlayer = Player.White;
            OnPlayerChanged();
        }

        private void ResetMovesInfo()
        {
            if (_movesInfo.ContainsKey(Player.White) == false)
            {
                _movesInfo.Add(Player.White, 0);
            }
            else
            {
                _movesInfo[Player.White] = 0;
            }

            if (_movesInfo.ContainsKey(Player.Black) == false)
            {
                _movesInfo.Add(Player.Black, 0);
            }
            else
            {
                _movesInfo[Player.Black] = 0;
            }

            OnMoveInfoChanged();
        }

        public void EndGame()
        {
            ResetBoard();
            _ugolkiExternalView.EndGame(_board);
        }

        public Player? CheckWinner()
        {
            int black = 0, white = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i >= _blackHousePosition && j >= _blackHousePosition && _board[i, j] == BoardCellType.White)
                    {
                        white += 1;
                    }
                    else if (i <= _whiteHousePosition &&
                             j <= _whiteHousePosition &&
                             _board[i, j] == BoardCellType.Black)
                    {
                        black += 1;
                    }
                }
            }

            if (black == 9)
            {
                return Player.Black;
            }

            if (white == 9)
            {
                return Player.White;
            }

            return null;
        }

        public void TrySelectCell(Coord cell)
        {
            if (_hasSelectedPiece == false && _board[cell.Row, cell.Column] == BoardCellType.Empty)
            {
                _ugolkiExternalView.ShowMessage(UgolkiMessages.SelectPiece);
                return;
            }

            if (_hasSelectedPiece == true && _selectedPiecePosition == cell)
            {
                _hasSelectedPiece = false;
                _ugolkiExternalView.DeselectPiece(cell);
                return;
            }

            if (_hasSelectedPiece == true && _board[cell.Row, cell.Column] == BoardCellType.Empty)
            {
                MovePiece(cell);
                return;
            }

            if (_board[cell.Row, cell.Column] == BoardCellType.White && _currentPlayer == Player.White ||
                _board[cell.Row, cell.Column] == BoardCellType.Black && _currentPlayer == Player.Black)
            {
                _hasSelectedPiece = true;
                _selectedPiecePosition = cell;

                _currentAvailableMoves = GetAvailableMoves(cell);
                _ugolkiExternalView.SelectPiece(cell, _currentAvailableMoves);
            }
            else
            {
                _ugolkiExternalView.ShowMessage(UgolkiMessages.NotYourMove);
            }
        }

        private void OnMoveInfoChanged()
        {
            MoveInfoChanged?.Invoke(_movesInfo);
        }

        private void ResetBoard()
        {
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    if (i <= _whiteHousePosition && j <= _whiteHousePosition)
                    {
                        _board[i, j] = BoardCellType.White;
                    }
                    else if (i >= _blackHousePosition && j >= _blackHousePosition)
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

        private List<Coord> GetAvailableMoves(Coord from)
        {
            Queue<Coord> toCheck = new Queue<Coord>();
            List<Coord> canJump = new List<Coord>();
            canJump.Add(from);
            toCheck.Enqueue(from);

            _availableMovesByRule[_currentRule].TryAddAvailableMoves(_board, from, toCheck, canJump);

            canJump.Remove(from);
            return canJump;
        }

        private void MovePiece(Coord cell)
        {
            if (_currentAvailableMoves.Contains(cell) == false)
            {
                _ugolkiExternalView.ShowMessage(UgolkiMessages.MoveUnreachable);
                return;
            }

            _board[_selectedPiecePosition.Row, _selectedPiecePosition.Column] = BoardCellType.Empty;

            BoardCellType resultCellType;
            if (_currentPlayer == Player.White)
            {
                resultCellType = BoardCellType.White;
            }
            else
            {
                resultCellType = BoardCellType.Black;
            }

            _hasSelectedPiece = false;
            _board[cell.Row, cell.Column] = resultCellType;

            Move move = new Move {IsJump = false, From = _selectedPiecePosition, To = cell};
            List<Move> moves = new List<Move> {move};

            _ugolkiExternalView.MovePiece(moves, OnMoveComplete);
        }

        private void OnMoveComplete()
        {
            _movesInfo[_currentPlayer]++;
            OnMoveInfoChanged();

            if (_currentPlayer == Player.White)
            {
                _currentPlayer = Player.Black;
            }
            else
            {
                _currentPlayer = Player.White;
            }

            OnPlayerChanged();

            Player? checkWinner = CheckWinner();
            if (checkWinner != null)
            {
                _ugolkiExternalView.ShowWinner(checkWinner);
            }
        }

        private void OnPlayerChanged()
        {
            PlayerChanged?.Invoke(_currentPlayer);
        }
    }
}