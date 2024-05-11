using UnityEngine;

public class ColorPoint : MonoBehaviour
{
    [SerializeField] private Material _material;
    [field: SerializeField] public int Weight { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }

    private bool _isColorChange;

    private void Start()
    {
        Inizialized();
    }

    private void OnValidate()
    {
        Inizialized();
    }

    public void SetColor(Color color)
    {
        if (_isColorChange) return;
        Color = color;
        _material.color = Color;
        _isColorChange = true;
    }

    private void Inizialized()
    {
        //_material ??= GetComponent<MeshRenderer>().material;
        //if (_material)
        //    _material.color = Color;
        _material ??= GetComponent<MeshRenderer>().sharedMaterials[0];
        if (_material)
        {
            Color color = Color;
            color.a = 1f;
            _material.color = color;
        }
    }

}
