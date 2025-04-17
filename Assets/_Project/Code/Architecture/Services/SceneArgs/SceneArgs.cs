using Zenject;

namespace _Project.Code
{
    public class SceneArgs : ISceneInputArgs, ISceneOutputArgs
    {
        public DiContainer Input { get; set; }
        public DiContainer Output { get; set; }
    }
}