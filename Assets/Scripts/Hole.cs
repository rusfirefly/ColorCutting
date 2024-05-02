using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Collected _collected;
    [SerializeField] private GameObject _holeComplete;

    private ColorPoint _holePoint;
    private Animation _animation;

    private void Start()
    {
        _holePoint = GetComponent<ColorPoint>();
        _animation = GetComponentInParent<Animation>();
        _collected.Initialized(_holePoint, _animation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HingeJoint joint = collision.gameObject.GetComponent<HingeJoint>();
        if(joint)
        {
            Destroy(joint);
        }
    }

    public void Complete()
    {
        _holeComplete.gameObject.SetActive(true);
    }

}
