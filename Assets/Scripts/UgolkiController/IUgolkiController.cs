using System;
using System.Collections.Generic;

namespace UgolkiController
{
    public interface IUgolkiController
    {
        event Action<Dictionary<Player, int>> MoveInfoChanged;
        List<string> GetRules();
        void SetRule(string rule);
        void StartGame();
        void EndGame();
        Player CheckWinner();
        void TrySelectPiece(Coord cell);
        void TryMovePiece(Coord from, Coord to, Player player);
    }
}