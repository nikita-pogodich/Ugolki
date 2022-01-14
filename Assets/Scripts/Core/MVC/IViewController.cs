namespace Core.MVC
{
    public interface IViewController
    {
        ViewType ViewType { get; }
        bool IsShown { get; }
        void SetShown(bool isShown);
    }
}