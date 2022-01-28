using System.Collections.Generic;
using System.Linq;

namespace UgolkiController.UgolkiRules
{
    public abstract class BaseUgolkiRule
    {
        public abstract void TryAddAvailableMoves(
            BoardCellType[,] board,
            Coord fromCell,
            Queue<Coord> toCheck,
            List<Coord> canJump);

        public abstract List<Coord> FindMoves(BoardCellType[,] board, Coord fromCell, Coord toCell);

        protected void TryAddAvailableJump(
            BoardCellType[,] board,
            Coord currentFrom,
            int row,
            int column,
            List<Coord> canJump,
            Queue<Coord> toCheck)
        {
            Coord currentTo;
            Coord currentTo2;

            try
            {
                currentTo = new Coord(currentFrom.Row + row, currentFrom.Column + column);
                currentTo2 = new Coord(currentFrom.Row + row * 2, currentFrom.Column + column * 2);
            }
            catch (OutOfBoardException)
            {
                return;
            }

            if (board[currentTo.Row, currentTo.Column] != BoardCellType.Empty &&
                board[currentTo2.Row, currentTo2.Column] == BoardCellType.Empty &&
                canJump.Contains(currentTo2) == false)
            {
                toCheck.Enqueue(currentTo2);
                canJump.Add(currentTo2);
            }
        }

        protected void TryAddAvailableMove(BoardCellType[,] board, Coord from, int row, int column, List<Coord> canJump)
        {
            Coord currentTo;

            try
            {
                currentTo = new Coord(from.Row + row, from.Column + column);
            }
            catch (OutOfBoardException)
            {
                return;
            }

            if (board[currentTo.Row, currentTo.Column] == BoardCellType.Empty &&
                canJump.Contains(currentTo) == false)
            {
                canJump.Add(currentTo);
            }
        }

        protected bool FindJumps(
            BoardCellType[,] board,
            Coord fromCell,
            Coord toCell,
            int row,
            int column,
            List<Coord> canJump,
            Queue<Coord> toCheck,
            List<Coord> moves)
        {
            toCheck.Enqueue(fromCell);
            moves.Clear();
            while (toCheck.Count > 0)
            {
                Coord currentFrom = toCheck.Dequeue();
                moves.Add(currentFrom);

                if (moves.LastOrDefault() == toCell)
                {
                    return true;
                }

                TryAddAvailableJump(board, currentFrom, row, column, canJump, toCheck);
            }

            return false;
        }

        protected bool FindMove(
            BoardCellType[,] board,
            Coord fromCell,
            Coord toCell,
            int row,
            int column,
            List<Coord> canJump,
            List<Coord> moves)
        {
            TryAddAvailableMove(board, fromCell, row, column, canJump);
            if (canJump.LastOrDefault() == toCell)
            {
                moves.Clear();
                moves.Add(fromCell);
                moves.Add(toCell);
                return true;
            }

            return false;
        }
    }
}