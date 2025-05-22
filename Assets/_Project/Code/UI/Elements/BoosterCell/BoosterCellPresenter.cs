using R3;
using UnityEngine;

namespace _Project.Code.UI.Elements.Booster
{
    public class SelectionHandler
    {
    }

    public class UseHandler
    {
    }


    public class BoosterCellPresenter
    {
        private readonly BoosterCellView _view;
        private readonly BoosterCell _model;

        public BoosterCellPresenter(
            BoosterCell model,
            BoosterCellView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.Count.Subscribe(_view.SetCount).AddTo(_view);
            _model.Id.Subscribe(_ => _view.SetIcon(_model.Icon)).AddTo(_view);
            _model.IsBlocked.Subscribe(_view.SetBlocked).AddTo(_view);
            _model.IsSelected.Subscribe(_view.SetSelected).AddTo(_view);
            _view.Clicked.Subscribe(_ => OnClicked()).AddTo(_view);

            _view.SetCount(_model.Count.Value);
            _view.SetIcon(_model.Icon);
            _view.SetBlocked(_model.IsBlocked.Value);
        }

        private void OnClicked()
        {
            if (!_model.IsBlocked.Value)
                _model.HandleClick();
        }
    }
}