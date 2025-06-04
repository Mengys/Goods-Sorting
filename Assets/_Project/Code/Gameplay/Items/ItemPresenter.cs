using _Project.Code.Gameplay.DragAndDrop;
using UnityEngine;
using DG.Tweening;

namespace _Project.Code.Gameplay.Items
{
    public class ItemPresenter
    {
        private readonly ItemView _view;
        private readonly ItemId _id;

        public ItemPresenter(ItemId id, ItemView view)
        {
            _id = id;
            _view = view;
        }

        public ItemId Id => _id;
        public Transform Transform => _view.transform;
        public IDragAndDropEvents DragAndDropEvents => _view.DragAndDropEvents;

        public void SetActive(bool value) => 
            _view.SetActive(value);
        
        public void Destroy()
        {
            Object.Destroy(_view.gameObject);
            _view.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    Object.Destroy(_view.gameObject);
                    Debug.Log(111);
                });
        }
    }
}