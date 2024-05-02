using System;
using UnityEngine;
using UnityEngine.UI;

public class Collected : MonoBehaviour
{
    [SerializeField] private Text _textCount;
    public static event Action CollectedPoint;
    public static event Action<int> ScoreAdd;
    private int _score;
    private ColorPoint _holeColor;
    private Animation _animation;

    private void OnCollisionEnter(Collision collision)
    {
        ColorPoint colorPoint = collision.gameObject.GetComponent<ColorPoint>();
        if(colorPoint)
        {
            if (_holeColor.Color == colorPoint.Color)
            {
                _score += colorPoint.Weight;
                ScoreAdd?.Invoke(_score);
                SetCountToText(_score);
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

        CollectedPoint?.Invoke();
        Destroy(collision.gameObject);
    }

    public void Initialized(ColorPoint holeColor, Animation animation)
    {
        _holeColor = holeColor;
        _animation = animation;
        _animation.Stop();
        SetCountToText(_score);
    }

    private void SetCountToText(int count) => _textCount.text = $"{count}";
}
