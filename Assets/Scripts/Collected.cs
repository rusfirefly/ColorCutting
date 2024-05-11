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
    private int _maxPoint;
    private int _pointNumber;
    private Hole _hole;
    private const int _multiply = 100;
    private void Awake()
    {
        _hole = GetComponentInParent<Hole>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ColorPoint colorPoint = collision.gameObject.GetComponent<ColorPoint>();
        if(colorPoint)
        {
           // Debug.Log($"{_holeColor.Color} == {colorPoint.Color}");
            if (_holeColor.Color == colorPoint.Color)
            {
                _score += colorPoint.Weight;
                _pointNumber++;
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

        if(_pointNumber == _maxPoint)
        {
            if (_hole)
              _hole.Complete();
        }

        CollectedPoint?.Invoke();
        Destroy(collision.gameObject);
    }

    public void Initialized(int maxPoint, ColorPoint holeColor, Animation animation)
    {
        _maxPoint = maxPoint;
        _holeColor = holeColor;
        _animation = animation;
        _animation.Stop();
        SetCountToText(_score);
    }

    private void SetCountToText(int count) => _textCount.text = $"{count}/{_hole.GetMaxPoint * _multiply}";
}
