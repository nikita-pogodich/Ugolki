namespace Core.MVC
{
    public interface IViewFactory
    {
        TView Create<TView>(string resourceKey) where TView : IView;
        void Destroy<TView>(string resourceKey, TView view) where TView : IView;
    }
}