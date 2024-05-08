using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LeveSelectHUD : MonoBehaviour
{
    [SerializeField] private Button _nextSeasonButton;
    [SerializeField] private Button _previewSeasonButton;
    [SerializeField] private Text _seasonName;
    [SerializeField] private Text _countStarCollectedText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Transform[] _seasons;
    [SerializeField] private Transform _seasonPosition;
    [SerializeField] private int _seasonNumber;

    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    private const int _countStarOnLevel = 3;
    private int _numberSeasonPreview;
    private Vector3 position;

    private void Awake()
    {
        position = _seasonPosition.position;
    }

    public void SetScoreText(int score) => _scoreText.text = $"Score: {score}";

    public void SetSeasonName(int seasonNumber) =>_seasonName.text = $"Season {seasonNumber}";

    public void SetStarCollected(int countStarCollectedAll, int countStarAll) =>_countStarCollectedText.text = $"{countStarCollectedAll}/{countStarAll * _countStarOnLevel}";

    public void SelectSeason(int numberSeason)
    {
        SetSeasonName(numberSeason);
        SetVisibleButton(numberSeason);
        
        if (numberSeason > _numberSeasonPreview)
        {
            position -= _pageStep;
        }
        else
        {
            position += _pageStep;
        }
        _seasonPosition.transform.DOMoveX(position.x, _duration).SetEase(_ease);
        _numberSeasonPreview = numberSeason;
    }

    private void SetVisibleButton(int numberSeason)
    {
        bool visible = false;
        if (numberSeason > 1)
        {
            visible = true;
        }

        SetVisibleButton(_previewSeasonButton, visible);
        
        if (numberSeason >= _seasons.Length)
            SetVisibleButton(_nextSeasonButton, false);
        else
            SetVisibleButton(_nextSeasonButton, true);
    }

    private void SetVisibleButton(Button button, bool visible)
    {
        button.gameObject.SetActive(visible);
    }


}
