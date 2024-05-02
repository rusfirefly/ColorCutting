using System;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    public event Action NullPoint;
    private int _maxPoint;

    public void Initialized(int maxPoint)
    {
        _maxPoint = maxPoint;
    }

    private void OnEnable()
    {
        Point.Destroed += OnDestroed;
        Collected.CollectedPoint += OnCollected;
    }

    private void OnDisable()
    {
        Point.Destroed -= OnDestroed;
        Collected.CollectedPoint -= OnCollected;
    }

    private void OnCollected()
    {
        _maxPoint--;
        CheckCountPoint();
    }

    private void OnDestroed()
    {
        _maxPoint--;
        CheckCountPoint();
    }

    private void CheckCountPoint()
    {

        if (_maxPoint == 0)
        {
            NullPoint?.Invoke();
        }
    }

}
