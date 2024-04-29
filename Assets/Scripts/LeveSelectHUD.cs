using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LeveSelectHUD : MonoBehaviour
{
    [SerializeField] private Button _nextSeasonButton;
    [SerializeField] private Button _previewSeasonButton;
    [SerializeField] private Text _seasonName;
    [SerializeField] private Text _countStarCollectedText;
    [SerializeField] private Transform _seasons;
    [SerializeField] private AnimationCurve _moveCurve;
    
    private const int _countStarOnLevel = 3;
    private float _move;

    [SerializeField] private int _seasonNumber;

    private void OnValidate()
    {
       // SelectSeason(_seasonNumber);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                _seasons.transform.DOMoveX(touch.position.x, 2f);
            }
        }
    }

    public void SetSeasonName(int seasonNumber) =>_seasonName.text = $"Season {seasonNumber}";

    public void SetStarCollected(int countStarCollectedAll, int countStarAll) =>_countStarCollectedText.text = $"{countStarCollectedAll}/{countStarAll * _countStarOnLevel}";

    public void SelectSeason(int numbreSeason)
    {
        float position = _seasons.transform.position.x;
        position = numbreSeason * 500;
        _seasons.transform.DOMoveX(position, 2f);
    }
}
