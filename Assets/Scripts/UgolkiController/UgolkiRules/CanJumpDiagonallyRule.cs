using System.Collections.Generic;

namespace UgolkiController.UgolkiRules
{
    public class CanJumpDiagonallyRule : BaseUgolkiRule
    {
        public override void TryAddAvailableMoves(
            BoardCellType[,] board,
            Coord from,
            Queue<Coord> toCheck,
            List<Coord> canJump)
        {
            while (toCheck.Count > 0)
            {
                Coord currentFrom = toCheck.Dequeue();

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            continue;
                        }

                        TryAddAvailableJump(board, currentFrom, i, j, canJump, toCheck);
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

                    TryAddAvailableMove(board, from, i, j, canJump);
                }
            }
        }
    }
}