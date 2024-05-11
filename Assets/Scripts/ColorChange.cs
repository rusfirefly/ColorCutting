using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _alpha;
    private Material _material;

    private void Start()
    {
        _material = _meshRenderer.materials[0];
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        //_material ??= _meshRenderer.sharedMaterials[0];
        if (_material)
        {
            Color color = _color;
            color.a = _alpha;
            _material.color = color;
        }

    }
    #endif

    private void OnTriggerEnter(Collider other)
    {
        ColorPoint point = other.GetComponent<ColorPoint>();
        if(point)
        {
            point.SetColor(_color);
        }
    }
}
