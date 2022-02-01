using System;
using Core.MVC;

namespace ViewControllers.UgolkiGame
{
    public interface IUgolkiGameView : IView
    {
        event Action Back;
        void SetWhiteMovesCount(string count);
        void SetBlackMovesCount(string count);
        void ChangeCurrentPlayer(string currentPlayer);
        void SetShown(bool isShown);
    }
}