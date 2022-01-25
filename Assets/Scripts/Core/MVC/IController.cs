using System;

namespace Core.MVC
{
    public interface IController : IDisposable
    {
        ViewType ViewType { get; }
        string Name { get; }
        bool IsShown { get; }
        void SetShown(bool isShown);
    }

    public interface IController<TView, TModel> : IController where TView : IView where TModel : IModel
    {
        TView View { get; }
        TModel Model { get; }
        void SetModel(TModel model);
        void SetView(TView view);
    }
}