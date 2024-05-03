using System;
using UnityEngine;

public class Star : MonoBehaviour
{
    public static event Action Collected;
    private bool _isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollected) return;
        Destroy(gameObject);
        Collected?.Invoke();
        _isCollected = true;
    }
}
