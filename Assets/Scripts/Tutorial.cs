using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image _hand1;
    [SerializeField] private Image _hand2;
    [SerializeField] private float _xMinPosition;
    [SerializeField] private float _xMaxPosition;
    [SerializeField] private float _speedAnimation;

    [SerializeField] private bool _isPlayAnimation;
    [SerializeField] private Transform _startPosition;

    private Vector3 _position;

    private void Start()
    {
        _position = _hand1.transform.localPosition;
        _isPlayAnimation = true;
        _hand1.transform.position = Camera.main.WorldToScreenPoint(_startPosition.position);
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            StopAnimation();
        }else
        {
            if(Input.GetMouseButtonDown(0))
            {
                StopAnimation();
            }
        }


        if(_isPlayAnimation)
        {
            _position.x = Mathf.PingPong(Time.time * _speedAnimation, _xMaxPosition - _xMinPosition) + _xMinPosition;
            _hand1.transform.localPosition = _position;
        }
    }

    public void StartAnimation() => _isPlayAnimation = true;

    public void StopAnimation()
    {
        _hand1.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
