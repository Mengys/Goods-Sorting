using _Project.Code.UI.Buttons.Window;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay
{
    public class CollectedItemsHandler : IItemCollectHandler
    {
        private readonly Subject<int> _itemsHandled = new();
        private readonly IScoreIncomeHandler _incomeHandler;

        public CollectedItemsHandler(IScoreIncomeHandler incomeHandler)
        {
            _incomeHandler = incomeHandler;
        }

        public Observable<int> ItemsHandled => _itemsHandled;
        
        public void Handle(int itemsCount = 1)
        {
            Debug.Log("ItemCollectHandler.Handle");
            
            _incomeHandler.Handle(itemsCount);
            _itemsHandled.OnNext(itemsCount);
        }
    }
}