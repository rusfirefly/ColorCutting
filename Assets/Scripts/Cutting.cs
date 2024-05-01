using System;
using System.Collections;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public event Action Lose;
    public event Action<int> Cut;

    [SerializeField] private Cord _cord;
    [SerializeField] private LayerMask _pointMask;
    [field:SerializeField] public int CountCut { get; private set; }
    [SerializeField] private GameObject _cutView;
    private float _zPosition;
    private HingeJoint point;
    private bool _isComplete;
    private bool _isCutVisible;
    private bool _isLastCut;
    private Vector3 _positionCursor;
    private bool _isTouch;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        if(_cord)
            _zPosition = _cord.transform.position.z;

        Cut?.Invoke(CountCut);
    }

    private void Update()
    {
        if (_isComplete) return;

        if (_isLastCut)
        {
            Lose?.Invoke();
            _isComplete = true;
        }

        if (Input.touchCount > 0)
        {
            _isTouch = true;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isCutVisible = true;
                    break;
                case TouchPhase.Ended:
                    _isCutVisible = false;
                    _isTouch = false;
                    break;
                default: break;
            }
            _positionCursor = touch.position;
            ShowCut(_isCutVisible);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                _isTouch = true;
                _positionCursor = Input.mousePosition;
                ShowCut(_isCutVisible);
                _isCutVisible = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isCutVisible = false;
                _isTouch = false;
            }
        }

        if (_isTouch)
        {

            Vector3 position = Camera.main.ScreenToWorldPoint(_positionCursor);
            position.z = _zPosition - 1f;

            if(_cutView)
            {
                _cutView.transform.position = position;
            }

            Debug.DrawRay(position, Vector3.forward, Color.red, 100f);
            if (Physics.Raycast(position, Vector3.forward, out RaycastHit hit, _pointMask))
            {
                point = hit.collider.gameObject.GetComponent<HingeJoint>();
                if (point && point.tag != "FixedPoint")
                {
                    Cord cord = point.GetComponent<Point>().Cord;
                    if (cord)
                    {
                        if (CountCut == 0)
                        {
                            _isLastCut = true;
                            return;
                        }

                        cord.Cut(point);
                        Destroy(point);
                        CutCord();
                    }
                }
            }
        }
    }

    public void AddCut(int count)
    {
        _isLastCut = false;
        _isComplete = false;
        CountCut += count;

        Cut?.Invoke(CountCut);
    }

    public void Complete()
    {
        _isComplete = true;
    }

    private void ShowCut(bool visible)
    {
        _cutView.gameObject.SetActive(visible);
    }

    private void CutCord()
    {
        CountCut--;
        if (CountCut >= 0)
            Cut?.Invoke(CountCut);
    }

    private IEnumerator WaitPointFly()
    {
        yield return new WaitForSeconds(1.5f);
        if (_isComplete == false)
        {
            Lose?.Invoke();
            _isComplete = true;
        }
    }
}
