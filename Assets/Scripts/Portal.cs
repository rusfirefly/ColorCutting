using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform _exitPosition;
    private Vector3 _position;
    [SerializeField] private float _offset;

    private void Start()
    {
        SetOffset();
        NewPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Point point = other.GetComponent<Point>();
        if(point)
        {
            if (point.IsPort) return;
            NewPosition();
            HingeJoint joint = other.GetComponent<HingeJoint>();
            if (joint)
            {
                Destroy(joint);
            }
            
            point.transform.position = _position;
            point.IsPort = true;

        }
    }

    private void NewPosition()
    {
        _position = _exitPosition.position;
        _exitPosition.position = _position;
    }
    private void SetOffset()
    {
        _position = _exitPosition.position;
        _position.y -= _offset;
    }
}
