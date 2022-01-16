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
        void TrySelectCell(Coord cell);
    }
}