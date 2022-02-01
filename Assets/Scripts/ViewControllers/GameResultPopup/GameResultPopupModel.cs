using System;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupModel : IGameResultPopupModel
    {
        private Player _player;

        public Player Player => _player;
        public event Action<Player> PlayerInfoChanged;
        private event Action Restart;
        private event Action BackToMenu;

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

            PlayerInfoChanged?.Invoke(player);
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