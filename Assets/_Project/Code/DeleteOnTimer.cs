using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOnTimer : MonoBehaviour
{
    [SerializeField] private float _timer = 0.5f;

    private void Update() {
        if (_timer < 0) Destroy(gameObject);
        _timer -= Time.deltaTime;
    }
}
