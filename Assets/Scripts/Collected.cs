using UnityEngine;
using UnityEngine.UI;

public class Collected : MonoBehaviour
{
    [SerializeField] private Text _textCount;
    private int _maxCount;
    private int _count;

    private void OnCollisionEnter(Collision collision)
    {
        _count++;
        SetCountToText(_count);
        if(_count >= _maxCount)
        {
            Debug.Log("WIN");
        }
        Destroy(collision.gameObject);
    }

    public void Initialized(int maxCount)
    {
        _maxCount = maxCount;
        SetCountToText(_count);
    }

    private void SetCountToText(int count) => _textCount.text = $"{count}/{_maxCount}";
}
