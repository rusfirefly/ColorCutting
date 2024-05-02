using UnityEngine;

public class ColorPoint : MonoBehaviour
{
    [SerializeField] private Material _material;

    [field: SerializeField] public Color Color { get; private set; }

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
        Color = color;
        _material.color = Color;
    }

    private void Inizialized()
    {
        _material ??= GetComponent<MeshRenderer>().material;
        if (_material)
            _material.color = Color;
    }

}
