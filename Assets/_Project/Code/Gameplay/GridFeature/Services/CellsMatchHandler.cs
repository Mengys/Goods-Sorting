using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using _Project.Code.Data.Static.Sound;
using _Project.Code.Gameplay.Shelves;
using R3;
using Solo.MOST_IN_ONE;
using UnityEngine;
using static Solo.MOST_IN_ONE.Most_HapticFeedback;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    public class CellsMatchHandler 
    {
        private readonly ItemInventory _itemInventory;
        private readonly Subject<int> _matchHandled = new();
        
        public CellsMatchHandler(ItemInventory itemInventory)
        {
            _itemInventory = itemInventory;
        }

        public Observable<int> MatchHandled => _matchHandled;

        public void Handle()
        {
            var itemPositions = _itemInventory.Cells.Keys;

            var shelfGroups =
                itemPositions.GroupBy(p => p.Shelf);

            foreach (var group in shelfGroups)
            {
                var layerGroups = group.GroupBy(p => p.Layer);

                foreach (var layerGroup in layerGroups)
                {
                    var positions = layerGroup.ToList();
                    
                    var items = positions
                        .Select(p => _itemInventory.Get(p)).ToList();
                    
                    bool match = items.All(i => i?.Id.Equals(items[0].Id) == true);

                    if (match)
                        OnMatchFound(positions);
                }
            }
        }

        private void OnMatchFound(List<CellGridPosition> positions)
        {
            Most_HapticFeedback.Generate(HapticTypes.LightImpact);
            SoundManager.Instance.PlaySound(SoundId.Match3);
            foreach (var position in positions) 
                _itemInventory.Clear(position);
            
            _matchHandled?.OnNext(positions.Count);
        }
    }
}