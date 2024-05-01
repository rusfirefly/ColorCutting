using System;
using UnityEngine;
using UnityEngine.UI;

public class Collected : MonoBehaviour
{
    [SerializeField] private Text _textCount;
    private Hole _hole;
    public static event Action Complete;
    public static event Action CollectedPoint;
    private int _maxCount;
    private int _count;
    private ColorPoint _holeColor;
    private Animation _animation;

    private void Start()
    {
        _hole = GetComponentInParent<Hole>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        ColorPoint colorPoint = collision.gameObject.GetComponent<ColorPoint>();
        if(colorPoint)
        {
            if (_holeColor.Color == colorPoint.Color)
            {
                _count++;
                SetCountToText(_count);
                if(_animation.isPlaying == false)
                _animation.Play();
            }
            else
            {
                Point point = collision.gameObject.GetComponent<Point>();
                if (point)
                {
                    point.CollectedEffect();
                }
            }
        }

        if (_count == _maxCount)
        {
            Complete?.Invoke();
            _hole.Complete();
        }

        CollectedPoint?.Invoke();
        Destroy(collision.gameObject);
    }

    public void Initialized(int maxCount, ColorPoint holeColor, Animation animation)
    {
        _holeColor = holeColor;
        _maxCount = maxCount;
        _animation = animation;

        _animation.Stop();

        SetCountToText(_count);
    }

    private void SetCountToText(int count) => _textCount.text = $"{count}/{_maxCount}";
}
