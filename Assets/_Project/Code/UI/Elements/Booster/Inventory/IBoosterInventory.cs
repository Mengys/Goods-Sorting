using _Project.Code.Data.Static.Booster;

namespace _Project.Code.UI.Elements.Booster.Inventory
{
    public interface IBoosterInventory
    {
        void Add(BoosterId id);
        bool Has(BoosterId id);
        void Remove(BoosterId id);
    }
}