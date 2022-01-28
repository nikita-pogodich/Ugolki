using System.Collections.Generic;

namespace UgolkiController.UgolkiRules
{
    public class CannotJumpRule : BaseUgolkiRule
    {
        public override void TryAddAvailableMoves(
            BoardCellType[,] board,
            Coord fromCell,
            Queue<Coord> toCheck,
            List<Coord> canJump)
        {
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    TryAddAvailableMove(board, fromCell, i, j, canJump);
                }
            }
        }

        public override List<Coord> FindMoves(
            BoardCellType[,] board,
            Coord fromCell,
            Coord toCell)
        {
            List<Coord> canJump = new List<Coord> {fromCell};
            List<Coord> moves = new List<Coord>();
            Queue<Coord> toCheck = new Queue<Coord>();
            toCheck.Enqueue(fromCell);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    if (FindMove(board, fromCell, toCell, i, j, canJump, moves))
                    {
                        return moves;
                    }
                }
            }

            return null;
        }
    }
}