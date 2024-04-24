using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cord : MonoBehaviour
{
    [SerializeField] private List<HingeJoint> _jointPositions;

    private LineRenderer _lineRenderer;
    private int _countPoint;

    public void Initialized()
    {
        NewCord();
    }

    private void FixedUpdate()
    {
        Draw();
    }
    
    private void NewCord()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        if (_jointPositions == null) return;

        HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();
        foreach (HingeJoint joint in joints)
        {
            _jointPositions.Add(joint);
        }

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
        CreateNewCord(joint);
    }

    private void CreateNewCord(HingeJoint joint)
    {
        int index = _jointPositions.IndexOf(joint);
        _countPoint = _lineRenderer.positionCount = index;
        
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
        for (int i = 0; i < _countPoint; i++)
        {
            _lineRenderer.positionCount = i + 1;

            if (_jointPositions[i] == null)
            {
                _jointPositions[i] = gameObject.AddComponent<HingeJoint>();
            }

            if (_jointPositions[i])
            {
                _lineRenderer.SetPosition(i, _jointPositions[i].transform.position);
            }
        }
    }
}
