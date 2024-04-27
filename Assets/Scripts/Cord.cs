using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cord : MonoBehaviour
{
    [SerializeField] private HingeJoint _startFixedPoint;
    [SerializeField] private HingeJoint _endFixedPoint;

    [SerializeField] private List<HingeJoint> _jointPositions;
    private LineRenderer _lineRenderer;
    private int _countPoint;

    public int CountPoint => _jointPositions.Count;

    public void Initialized()
    {
        _jointPositions = new List<HingeJoint>();
        NewCord();
    }

    public int GetCountFixedPoint()
    {
        int countFixed = 0;
        if(_startFixedPoint)
        {
            countFixed++;
        }
        if(_endFixedPoint)
        {
            countFixed++;
        }
        return countFixed;
    }

    private void FixedUpdate()
    {
        Draw();
    }
    
    private void NewCord()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();

        if (_startFixedPoint)
        {
            Cord cord = joints[0].GetComponent<Point>().Cord;
            _startFixedPoint.GetComponent<Point>().Cord = cord;
            _jointPositions.Add(_startFixedPoint);
            joints[0].connectedBody = _startFixedPoint.GetComponent<Rigidbody>();
        }

        foreach (HingeJoint joint in joints)
        {
            _jointPositions.Add(joint);
        }

        if (_endFixedPoint)
        {
            Cord cord = GetCord(_jointPositions.Count - 1);
            _endFixedPoint.connectedBody = _jointPositions[_jointPositions.Count-1].GetComponent<Rigidbody>();
            _endFixedPoint.GetComponent<Point>().Cord = cord;
            _jointPositions.Add(_endFixedPoint);
        }

        if (_jointPositions == null) return;
        _countPoint = _jointPositions.Count;
        _lineRenderer.positionCount = _countPoint;
    }
    
    public void CreateCord(List<HingeJoint> joints, float width, Material material, Gradient color)
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _lineRenderer.endWidth = width;
        _lineRenderer.material = material;
        _lineRenderer.colorGradient = color;

        _jointPositions = joints;
        _countPoint = _jointPositions.Count;
        _lineRenderer.positionCount = _countPoint;
    }

    public void Cut(HingeJoint joint)
    {
        if (_jointPositions == null) return;
        int index = _jointPositions.IndexOf(joint);
        int count = _jointPositions.Count - index;
        CreateNewCord(joint, index);
        _jointPositions.RemoveRange(index, count);
        _countPoint = _jointPositions.Count;
    }

    public void Clear()
    {
        _countPoint = _lineRenderer.positionCount = 0;
        _jointPositions.Clear();
    }

    private Cord GetCord(int index)=> _jointPositions[index].GetComponent<Point>().Cord;

    private void CreateNewCord(HingeJoint joint, int index)
    {
        Cord cord = joint.gameObject.AddComponent<Cord>();
        List<HingeJoint> joints = new List<HingeJoint>();
        for (int i = index; i < _jointPositions.Count; i++)
        {
            HingeJoint jointPoint = _jointPositions[i];
            jointPoint.GetComponent<Point>().Cord = cord;
            joints.Add(jointPoint);
        }
        cord.CreateCord(joints, _lineRenderer.startWidth, _lineRenderer.material, _lineRenderer.colorGradient);
    }

    private void Draw()
    {
        if (_jointPositions == null) return;
        for (int i = 0; i < _countPoint; i++)
        {
            _lineRenderer.positionCount = i + 1;
            _lineRenderer.SetPosition(i, _jointPositions[i] == null ? gameObject.transform.position : _jointPositions[i].transform.position);
        }
    }
}
