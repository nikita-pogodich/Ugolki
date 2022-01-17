using System.Collections.Generic;
using Core.Managers.LocalizationManager;
using Core.Managers.ViewManager;
using Core.MVC;
using Settings;
using Settings.LocalizationKeys;
using UgolkiController;

namespace ViewControllers.UgolkiGame
{
    public class UgolkiGameViewController : BaseViewController<UgolkiGameView, ViewModel>
    {
        public override ViewType ViewType => ViewType.Window;
        public override string Name => ViewNamesList.UgolkiGame;

        private IUgolkiController _ugolkiController;
        private IViewManager _viewManager;
        private ILocalizationManager _localizationManager;

        public UgolkiGameViewController(
            IViewManager viewManager,
            IUgolkiController ugolkiController,
            ILocalizationManager localizationManager)
        {
            _ugolkiController = ugolkiController;
            _viewManager = viewManager;
            _localizationManager = localizationManager;
        }

        protected override void OnViewAdded()
        {
            _ugolkiController.MoveInfoChanged += OnMoveInfoChanged;
            _ugolkiController.PlayerChanged += OnCurrentPlayerChanged;
            this.View.Back += OnBack;
        }

        protected override void OnViewRemoved()
        {
            _ugolkiController.MoveInfoChanged -= OnMoveInfoChanged;
            _ugolkiController.PlayerChanged -= OnCurrentPlayerChanged;
            this.View.Back -= OnBack;
        }

        protected override void OnSetShown(bool isShown)
        {
            this.View.SetShown(isShown);
        }

        private void OnMoveInfoChanged(Dictionary<Player, int> movesInfo)
        {
            string whiteMovesText = GetMovesText(movesInfo[Player.White], UgolkiGameLocalizationKeys.WhiteMovesCount);
            this.View.SetWhiteMovesCount(whiteMovesText);

            string blackMovesText = GetMovesText(movesInfo[Player.Black], UgolkiGameLocalizationKeys.BlackMovesCount);
            this.View.SetBlackMovesCount(blackMovesText);
        }

        private string GetMovesText(int count, string localizationKey)
        {
            string movesCountText = _localizationManager.GetText(
                key: localizationKey,
                keyToReplace: UgolkiGameLocalizationKeys.MovesCountValue,
                valueToReplace: count.ToString());

            return movesCountText;
        }

        private void OnCurrentPlayerChanged(Player player)
        {
            string result;
            if (player == Player.White)
            {
                result = UgolkiGameLocalizationKeys.WhitePlayer;
            }
            else
            {
                result = UgolkiGameLocalizationKeys.BlackPlayer;
            }

            string currentPlayerText = _localizationManager.GetText(
                key: UgolkiGameLocalizationKeys.CurrentPlayer,
                keyToReplace: UgolkiGameLocalizationKeys.CurrentPlayerValue,
                valueToReplace: result);

            this.View.ChangeCurrentPlayer(currentPlayerText);
        }

        private void OnBack()
        {
            _ugolkiController.EndGame();
            _viewManager.ShowView(ViewNamesList.MainMenu);
        }
    }
}