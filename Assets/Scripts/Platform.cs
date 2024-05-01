using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool _isPlayAnimation;
    [SerializeField] private float _speedAnimation;
    [SerializeField] private float _xPosition;

    private void Start()
    {
        if(_isPlayAnimation)
            transform.DOMoveX(_xPosition, _speedAnimation).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}
