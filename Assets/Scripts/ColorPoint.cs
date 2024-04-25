using UnityEngine;

public class ColorPoint : MonoBehaviour
{
    [SerializeField] private Material _material;

    [field: SerializeField] public Color Color { get; private set; }

    private void OnValidate()
    {
        _material ??= GetComponent<MeshRenderer>().material;
        if (_material)
            _material.color = Color;
    }
}
