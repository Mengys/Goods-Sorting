using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Elements.Booster.Factory;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements.Booster.Installers
{
    public class UsableBoosterInventoryInstaller : MonoBehaviour
    {
        private IProgressProvider _progress;
        
        private BoosterInventoryFactory _inventoryFactory;
        private DiContainer _context;

        [Inject]
        public void Construct(BoosterInventoryFactory factory, DiContainer context)
        {
            _context = context;
            _inventoryFactory = factory;
        }

        private void Awake() => 
            _inventoryFactory.CreateForGameplay(transform, _context);
    }
}