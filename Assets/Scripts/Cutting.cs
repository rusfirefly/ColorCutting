using UnityEngine;

public class Cutting : MonoBehaviour
{
    [SerializeField] private Cord _cord;
    [SerializeField] private LineRenderer _lineCut;
    [SerializeField] private LayerMask _pointMask;
    [SerializeField] private int _countCut;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _zPosition;

    private void Start()
    {
        _lineCut = GetComponent<LineRenderer>();
        _lineCut.positionCount = 2;
        _zPosition = _cord.transform.position.z - Camera.main.transform.position.z;
    }
    HingeJoint point;
    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zPosition - 2f;

        if (_countCut < 0)
        {
            Debug.Log("Lose");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _countCut--;
            _startPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _lineCut.positionCount = 2;
            _lineCut.SetPosition(0, _startPosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (_lineCut.positionCount == 0) return;
            _endPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _lineCut.SetPosition(1, _endPosition);
            Debug.DrawRay(_endPosition, Vector3.forward, Color.red, 100f);
            if (Physics.Raycast(_endPosition, Vector3.forward, out RaycastHit hit, _pointMask))
            {
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
       
    }
   
}
