using System;
using Core.MVC;
using UnityEngine;

namespace ViewControllers.MainMenu.UgolkiRulesListItem
{
    public interface IUgolkiRulesListItemView : IView
    {
        GameObject ItemView { get; }
        event Action Selected;
        void SetSelected(bool isSelected);
        void SetTitle(string title);
    }
}