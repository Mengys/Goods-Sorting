using _Project.Code.Architecture.Services.LevelGeneration;
using UnityEngine;
using Zenject;

namespace _Project.Code
{    
    public class GameplayBootstrapper : MonoInstaller
    {
        [Inject] private ISceneInputArgs _inputArgs;
        
        private void Awake()
        {
            var message = _inputArgs.Input.Resolve<string>();
            
            Debug.Log(message);
            
            Container.Resolve<LevelGenerator>().Generate();
        }
        
        public override void InstallBindings()
        {
            Container.Bind<LevelGenerator>().AsSingle();
        }

        private void Update()
        {
            
        }
    }
}