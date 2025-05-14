using _Project.Code.Gameplay.GridFeature.Services;
using R3;

namespace _Project.Code.Gameplay.GridFeature
{
    public interface IGrid
    {
        Observable<int> MatchCollected { get; }
        Observable<Unit> AllMatchesCollected { get; }
        Observable<Unit> FirstMoveMade { get; }
        Observable<Unit> FirstLayerFilled { get; }
        
        ItemInventory ItemInventory { get; }
        
        void Disable();
        void Enable();
        void Initialize();
    }
}