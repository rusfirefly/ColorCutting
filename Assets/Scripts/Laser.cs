using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _maskPoint;
    [SerializeField] private float _timeReload;
    [SerializeField] private bool _isShoot;
    [SerializeField] private bool _timerOff;

    private LineRenderer _lineRender;
    private Vector3 _position;
    
    private float _currentTime;

    private void Start()
    {
        Inizialized();
    }

    private void OnValidate()
    {
        Inizialized();
    }

    private void Update()
    {
        if (_timerOff == false)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _timeReload)
            {
                _isShoot = !_isShoot;
                _lineRender.enabled = _isShoot;
                _currentTime -= _timeReload;
            }
        }

        if (_isShoot)
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(new Ray(transform.position, Vector3.right), _distance, _maskPoint);
            foreach (RaycastHit raycastHit in raycastHits)
            {
                Point point = raycastHit.collider.GetComponent<Point>();
                if (point)
                {
                    point.Destroy();
                }
            }
        }
    }
    private void Inizialized()
    {
        _lineRender ??= GetComponent<LineRenderer>();
        _position = transform.position;
        _position.x += _distance;
        _lineRender.SetPosition(0, transform.position);
        _lineRender.SetPosition(1, _position);
        Debug.DrawRay(transform.position, Vector3.right, Color.red, 100f);

    }
}
