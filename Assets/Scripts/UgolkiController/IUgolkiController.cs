using System.Collections.Generic;

namespace UgolkiController
{
    public interface IUgolkiController
    {
        BoardCellType[,] Board { get; }
        List<string> GetRules();
        void SetRule(string rule);
        void StartGame();
        void EndGame();
        Player CheckWinner();
        bool TrySelectPiece(Coord cell, out string errorType);
        bool TryMovePiece(Coord from, Coord to, Player player, string errorType);
    }
}