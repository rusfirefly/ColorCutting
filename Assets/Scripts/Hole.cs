using UnityEngine;
using UnityEngine.UI;

public class Hole : MonoBehaviour
{
    [SerializeField] private Collected _collected;
    [SerializeField] private int _maxCount;

    private void Start()
    {
        _collected.Initialized(_maxCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HingeJoint joint = collision.gameObject.GetComponent<HingeJoint>();
        if(joint)
        {
            Destroy(joint);
        }
    }


}
