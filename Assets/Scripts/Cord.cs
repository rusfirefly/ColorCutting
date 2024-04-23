using UnityEngine;

public class Cord : MonoBehaviour
{
    [field: SerializeField] public Transform[] positions { get; private set; }
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = positions.Length;
    }

    private void FixedUpdate()
    {
        Draw();
    }

    private void Draw()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            _lineRenderer.positionCount = i + 1;
            _lineRenderer.SetPosition(i, positions[i].position);
        }
    }
}
