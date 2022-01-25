using Core.MVC;
using UnityEngine;
using ViewControllers.MainMenu.UgolkiRulesListItem;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public class UgolkiRulesListView : BaseView, IUgolkiRulesListView
    {
        [SerializeField]
        private RectTransform _content;

        public void AddItem(IUgolkiRulesListItemView item)
        {
            Transform itemTransform = item.ItemView.transform;
            itemTransform.SetParent(_content);
            itemTransform.localScale = Vector3.one;
            itemTransform.localEulerAngles = Vector3.zero;
        }
    }
}