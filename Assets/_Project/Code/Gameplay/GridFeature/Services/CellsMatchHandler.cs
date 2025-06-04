using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using R3;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    public class CellsMatchHandler
    {
        private readonly ItemInventory _itemInventory;
        private readonly Subject<int> _matchHandled = new();
        [SerializeField] private AudioSource _mergeAudio;

        public CellsMatchHandler([CanBeNull] ItemInventory itemInventory)
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
            Handheld.Vibrate();

            var audioSource = GameObject.Find("mergeAudio")?.GetComponent<AudioSource>();
            audioSource?.Play();

            var starSpawner = UnityEngine.Object.FindObjectOfType<StarSpawner>();

            foreach (var position in positions)
            {
                var item = _itemInventory.Get(position);
                if (item != null)
                {
                    Vector3 worldPos = item.Transform.position;

                    item.Transform
                        .DOScale(1.2f, 0.1f)
                        .SetLoops(2, LoopType.Yoyo)
                        .SetEase(Ease.InOutSine)
                        .OnComplete(() => {
                            _itemInventory.Clear(position);
                            starSpawner?.SpawnStar(worldPos);
                        });
                }
                else
                {
                    _itemInventory.Clear(position);
                }
            }

            _matchHandled?.OnNext(positions.Count);
        }

    }
}