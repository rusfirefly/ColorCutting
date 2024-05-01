using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private Color _color;

    private void OnTriggerEnter(Collider other)
    {
        ColorPoint poin = other.GetComponent<ColorPoint>();
        if(poin)
        {
            Debug.Log(poin.name);
            poin.SetColor(_color);
        }
    }
}
