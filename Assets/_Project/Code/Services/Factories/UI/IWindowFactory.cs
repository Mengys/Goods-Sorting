using _Project.Code.Data.Static.Booster;
using _Project.Code.UI.Elements.Booster.Inventory;
using _Project.Code.UI.Windows.Base;
using _Project.Code.UI.Windows.Implementations;

namespace _Project.Code.Services.Factories.UI
{
    public interface IWindowFactory
    {
        Window Create(WindowId id);
        Window CreateBuyBooster(BoosterId id, IBoosterInventory inventory);
    }
}