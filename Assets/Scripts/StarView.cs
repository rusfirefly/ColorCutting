using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    [SerializeField] private Image[] _stars;
    [SerializeField] private Sprite _startCollected;
    [SerializeField] private Sprite _noneStar;

    private void Awake()
    {
        SetDefaultStartCollected();
    }

    public void SetStar(int index)
    {
        _stars[index].sprite = _startCollected;
    }

    private void SetDefaultStartCollected()
    {
        for(int i = 0; i < _stars.Length; i++)
        {
            _stars[i].sprite = _noneStar;
        }
    }
}
