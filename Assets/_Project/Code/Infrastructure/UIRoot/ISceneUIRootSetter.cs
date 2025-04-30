namespace _Project.Code.Infrastructure.UIRoot
{
    public interface ISceneUIRootSetter
    {
        void Set(IUIRoot sceneUIRoot);   
        void Cleanup();
    }
}