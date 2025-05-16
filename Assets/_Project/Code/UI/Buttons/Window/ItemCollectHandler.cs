using R3;
using UnityEngine;

namespace _Project.Code.UI.Buttons.Window
{
    public class ItemCollectHandler : IItemCollectHandler
    {
        private readonly Subject<int> _itemsHandled = new();
        private readonly IScoreIncomeHandler _incomeHandler;

        public ItemCollectHandler(IScoreIncomeHandler incomeHandler)
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