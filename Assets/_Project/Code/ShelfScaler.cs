using _Project.Code.Infrastructure.UIRoot.Implementations;
using _Project.Code.Services.ProgressProvider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShelfScaler : MonoBehaviour {
    [Inject] private GameplayUIRoot _gameplayUIRoot;

    private void Start() {
        if (_gameplayUIRoot.Container.Resolve<IProgressProvider>().PlayerProgress.Level.Number == 1) {
            ScaleShelfs(1.2f);
        } else {
            ScaleShelfs(0.8f);
        }
    }

    private void ScaleShelfs(float scale) {
        var shelfs = GameObject.FindGameObjectsWithTag("Shelf");
        foreach (var shelf in shelfs) {
            shelf.transform.localScale = new Vector3(scale, scale, scale);

            var pos = shelf.transform.position;
            var center = transform.position;

            pos = center + (pos - center) * scale;
            shelf.transform.position = pos;
        }
    }
}
