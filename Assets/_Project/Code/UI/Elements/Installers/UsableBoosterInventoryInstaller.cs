using _Project.Code.Services.ProgressProvider;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class UsableBoosterInventoryInstaller : MonoBehaviour
    {
        private IProgressProvider _progress;
        private BoosterInventoryFactory _inventoryFactory;

        [Inject]
        public void Construct(BoosterInventoryFactory factory)
        {
            _inventoryFactory = factory;
        }

        private void Awake()
        {
            _inventoryFactory.CreateForGameplay(transform);
        }
    }
}