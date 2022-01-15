namespace Core.MVC
{
    public interface IViewController<in TModel>
    {
        ViewType ViewType { get; }
        string Name { get; }
        bool IsShown { get; }
        void SetShown(bool isShown);
        void SetModel(TModel model);
    }

    public interface IViewController : IViewController<ViewModel>
    { }
}