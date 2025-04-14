using UnityEngine;
using Zenject;

namespace _Project.Code
{
    public class GameBootstrapper : MonoInstaller
    {
        public override void InstallBindings()
        {
        }

        private void Awake()
        {
            Debug.Log("Entered GameBootstrapper");
        }
    }
}