using System;
using System.Collections;
using UnityEngine;

public class Cutting : MonoBehaviour
{
    public event Action Lose;
    public event Action<int> Cut;

    [SerializeField] private Cord _cord;
    [SerializeField] private LineRenderer _lineCut;
    [SerializeField] private LayerMask _pointMask;
    [SerializeField] private int _countCut;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _zPosition;
    private HingeJoint point;
    private bool _isComplete;
    private bool _isCut;

    private void Start()
    {
        _lineCut = GetComponent<LineRenderer>();
        _lineCut.positionCount = 2;
        _zPosition = _cord.transform.position.z - Camera.main.transform.position.z;
        Cut?.Invoke(_countCut);
    }

    private void Update()
    {
        if (_isComplete) return;

        if (_countCut < 0 && _isCut)
        {
            StartCoroutine(WaitPointFly());
            _isCut = false;
            return;
        }



        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zPosition - 2f;

        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _lineCut.positionCount = 2;
            _lineCut.SetPosition(0, _startPosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (_lineCut.positionCount <=1 ) return;
            _endPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _lineCut.SetPosition(1, _endPosition);
            Debug.DrawRay(_endPosition, Vector3.forward, Color.red, 100f);

            if (Physics.Raycast(_endPosition, Vector3.forward, out RaycastHit hit, _pointMask))
            {
                if(_isCut == false)
                {
                    CutCord();
                    _isCut = true;
                }

                point = hit.collider.gameObject.GetComponent<HingeJoint>();
                if (point && point.tag != "FixedPoint")
                {
                    Cord cord = point.GetComponent<Point>().Cord;
                    if (cord)
                    {
                        cord.Cut(point);
                        Destroy(point);
                        _lineCut.positionCount = 0;
                    }
                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            if (_isCut == false)
            {
                CutCord();
                _lineCut.positionCount = 0;
            }
        }

        if (_countCut < 0)
        {
            Lose?.Invoke();
        }
    }


    public void Complete()
    {
        _isComplete = true;
    }

    private void CutCord()
    {
        _countCut--;
        if (_countCut >= 0)
            Cut?.Invoke(_countCut);
    }

    private IEnumerator WaitPointFly()
    {
        yield return new WaitForSeconds(5f);
        if(_isComplete == false)
            Lose?.Invoke();
    }
}
