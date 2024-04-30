using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _xMinPosition;
    [SerializeField] private float _xMaxPosition;
    [SerializeField] private float _speedAnimation;

    [SerializeField] private bool _isPlayAnimation;
    [SerializeField] private Transform _startPosition;

    private Vector3 _position;

    private void Start()
    {
        _position = _image.transform.localPosition;
        _isPlayAnimation = true;
      
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            StopAnimation();
        }

        if(_isPlayAnimation)
        {
            _position.x = Mathf.PingPong(Time.time * _speedAnimation, _xMaxPosition - _xMinPosition) + _xMinPosition;
            _image.transform.localPosition = _position;
        }
    }

    private void OnValidate()
    {
        _image.transform.position = Camera.main.WorldToScreenPoint(_startPosition.position);
    }

    public void StartAnimation() => _isPlayAnimation = true;

    public void StopAnimation()
    {
        _image.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
