using Zenject;

namespace _Project.Code.Infrastructure.Services.SceneArgs
{
    public class SceneArgs : ISceneInputArgs, ISceneOutputArgs
    {
        public DiContainer Input { get; set; } = new();
        public DiContainer Output { get; set; } = new();
    }
}