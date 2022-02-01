using System;
using Core.MVC;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public interface IGameResultPopupModel : IModel
    {
        Player Player { get; }
        event Action<Player> PlayerInfoChanged; 
        void UpdateModel(Player player, Action restart, Action exit);
        void OnRestart();
        void OnBackToMenu();
    }
}