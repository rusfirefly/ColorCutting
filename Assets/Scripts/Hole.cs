using System;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Collected _collected;
    [SerializeField] private GameObject _holeComplete;
    [SerializeField] private int _maxPoint;
    private ColorPoint _holePoint;
    private Animation _animation;
    public static event Action Completed;

    private void Start()
    {
        _holePoint = GetComponent<ColorPoint>();
        _animation = GetComponentInParent<Animation>();
        _collected.Initialized(_maxPoint, _holePoint, _animation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HingeJoint joint = collision.gameObject.GetComponent<HingeJoint>();
        if(joint)
        {
            Destroy(joint);
        }
    }

    public int GetMaxPoint => _maxPoint;

    public void Complete()
    {
        Completed?.Invoke();
        _holeComplete.gameObject.SetActive(true);
    }

    

}
