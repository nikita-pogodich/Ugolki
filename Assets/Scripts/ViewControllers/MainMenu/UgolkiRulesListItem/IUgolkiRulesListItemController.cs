using System;
using Core.MVC;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public interface IUgolkiRulesListItemController :
        IController<IUgolkiRulesListItemView, IUgolkiRulesListItemModel>
    {
        event Action<IUgolkiRulesListItemController> Selected;
        void SetSelected(bool isSelected);
        void OnLocalizationChanged();
    }
}