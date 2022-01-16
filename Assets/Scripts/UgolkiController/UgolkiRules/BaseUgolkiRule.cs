using System.Collections.Generic;

namespace UgolkiController.UgolkiRules
{
    public abstract class BaseUgolkiRule
    {
        public abstract void TryAddAvailableMoves(
            BoardCellType[,] board,
            Coord from,
            Queue<Coord> toCheck,
            List<Coord> canJump);

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
    }
}