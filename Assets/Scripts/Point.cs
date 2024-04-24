using UnityEngine;

public class Point : MonoBehaviour
{
    [field: SerializeField] public Cord Cord { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}
