using UnityEditor;
using UnityEngine;

//[RequireComponent(typeof(LineRenderer))]
public class Cutting : MonoBehaviour
{
    [SerializeField] private Cord _cord;
    [SerializeField] private LineRenderer _lineCut;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _zPosition;
    private void Start()
    {
        _lineCut = GetComponent<LineRenderer>();
        _lineCut.positionCount = 2;
        _zPosition = _cord.transform.position.z - Camera.main.transform.position.z;
        //_lineCut.startWidth = _lineCut.endWidth = 0.05f;
    }

    private void Update()
    {
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
            _endPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            _lineCut.SetPosition(1, _endPosition);
            Debug.DrawRay(_endPosition, Vector3.forward, Color.red, 100f);
            if(Physics.Raycast(_endPosition, Vector3.forward, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                HingeJoint point = hit.collider.gameObject.GetComponent<HingeJoint>();
                
                if (point)
                {
                    Destroy(point);

                }
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
           // FindObjectsBelowLine();
            _lineCut.positionCount = 0;
        }
    }

    private void Cut()
    {
        
    }

    private void OnDrawGizmos()
    {
        
    }

    void FindObjectsBelowLine()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = _zPosition;
        RaycastHit[] raycastHits = Physics.RaycastAll(Camera.main.ScreenToWorldPoint(mousePosition), Vector3.zero);

        foreach(RaycastHit raycastHit in raycastHits)
        {
            Debug.Log(raycastHit.collider.gameObject.name);
        }
        
    }

}
