using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cord : MonoBehaviour
{
    private List<HingeJoint> _jointPositions;
    private LineRenderer _lineRenderer;
    private int _countPoint;

    public void Initialized()
    {
        _jointPositions = new List<HingeJoint>();
        NewCord();
    }

    private void FixedUpdate()
    {
        Draw();
    }
    
    private void NewCord()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();
        foreach (HingeJoint joint in joints)
        {
            _jointPositions.Add(joint);
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

    private void CreateNewCord(HingeJoint joint, int index)
    {
        Cord cord = joint.gameObject.AddComponent<Cord>();
        List<HingeJoint> joints = new List<HingeJoint>();

        for (int i = index; i < _jointPositions.Count; i++)
        {
            HingeJoint jointPoint = _jointPositions[i];
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
