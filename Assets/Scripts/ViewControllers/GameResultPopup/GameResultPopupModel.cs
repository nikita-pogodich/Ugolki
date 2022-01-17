using Core.MVC;
using UgolkiController;

namespace ViewControllers.GameResultPopup
{
    public class GameResultPopupModel : ViewModel
    {
        private Player _player;

        public Player Player => _player;

        public GameResultPopupModel()
        { }

        public GameResultPopupModel(Player player)
        {
            UpdateModel(player);
        }

        public void UpdateModel(Player player)
        {
            _player = player;
        }
    }
}