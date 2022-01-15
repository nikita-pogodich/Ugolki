using System.Collections.Generic;
using Core.Managers.Logger;

namespace UgolkiController
{
    public interface IUgolkiController
    {
        BoardCellType[,] Board { get; }
        void Initialize(IUgolkiExternalViewController externalViewController, ILogger logger);
        List<string> GetRules();
        void SetRule(string rule);
        void StartGame();
        void EndGame();
        Player CheckWinner();
        bool TrySelectPiece(Coord cell, out string errorType);
        bool TryMovePiece(Coord from, Coord to, Player player, string errorType);
    }
}