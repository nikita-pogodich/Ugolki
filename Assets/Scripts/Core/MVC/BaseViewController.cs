using UnityEngine;

namespace Core.MVC
{
    public abstract class BaseViewController<TView, TModel> : IViewController<TModel>
        where TView : MonoBehaviour
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

        public void SetModel(TModel model)
        {
            _model = model;
            _hasModel = true;
            OnSetModel();
        }

        public void RemoveModel()
        {
            _hasModel = false;
            OnRemoveModel();
        }

        public void SetView(TView view)
        {
            _view = view;
            _hasView = true;
            OnViewAdded();
        }

        public void RemoveView()
        {
            _view = null;
            _hasView = false;
            OnViewRemoved();
        }

        public virtual void SetShown(bool isShown)
        {
            _isShown = isShown;
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

        protected virtual void OnDispose()
        { }
    }
}