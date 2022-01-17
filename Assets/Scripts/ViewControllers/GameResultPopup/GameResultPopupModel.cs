using System;
using Core.MVC;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupModel : ViewModel
    {
        private Player _player;

        public Player Player => _player;
        public event Action Restart;
        public event Action BackToMenu;

        public GameResultPopupModel()
        { }

        public GameResultPopupModel(Player player, Action restart, Action exit)
        {
            UpdateModel(player, restart, exit);
        }

        public void UpdateModel(Player player, Action restart, Action exit)
        {
            _player = player;
            Restart = restart;
            BackToMenu = exit;
        }

        public void OnRestart()
        {
            Restart?.Invoke();
        }

        public void OnBackToMenu()
        {
            BackToMenu?.Invoke();
        }
    }
}