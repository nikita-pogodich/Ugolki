namespace Core.MVC
{
    public abstract class BaseController<TView, TModel> : IController<TView, TModel>
        where TView : IView
        where TModel : IModel
    {
        private TView _view;
        private TModel _model;
        private bool _hasView;
        private bool _hasModel;
        private bool _isShown;

        public virtual ViewType ViewType => ViewType.Simple;
        public virtual string Name => string.Empty;

        public TView View => _view;
        public TModel Model => _model;
        public bool HasView => _hasView;
        public bool HasModel => _hasModel;
        public bool IsShown => _isShown;

        protected BaseController(TView view, TModel model)
        {
            SetView(view);
            SetModel(model);
        }

        public virtual void SetShown(bool isShown)
        {
            _isShown = isShown;
            OnSetShown(_isShown);
        }

        public void Dispose()
        {
            RemoveView();
            RemoveModel();
            OnDispose();
        }

        protected virtual void OnSetModel()
        { }

        protected virtual void OnRemoveModel()
        { }

        protected virtual void OnViewAdded()
        { }

        protected virtual void OnViewRemoved()
        { }

        protected virtual void OnSetShown(bool isShown)
        { }

        protected virtual void OnDispose()
        { }

        private void SetModel(TModel model)
        {
            _model = model;
            _hasModel = true;
            OnSetModel();
        }

        private void RemoveModel()
        {
            _hasModel = false;
            OnRemoveModel();
        }

        private void SetView(TView view)
        {
            _view = view;
            _hasView = true;
            OnViewAdded();
        }

        private void RemoveView()
        {
            _hasView = false;
            OnViewRemoved();
        }
    }
}