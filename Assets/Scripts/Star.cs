using System;
using UnityEngine;

public class Star : MonoBehaviour
{
    public static event Action Collected;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Collected?.Invoke();
    }
}
