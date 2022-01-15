using UnityEngine;

namespace ViewControllers.MainMenu.UgolkiRulesList
{
    public class UgolkiRulesListView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _content;

        public void AddItem(GameObject item)
        {
            Transform itemTransform = item.transform;
            itemTransform.SetParent(_content);
            itemTransform.localScale = Vector3.one;
            itemTransform.localEulerAngles = Vector3.zero;
        }
    }
}