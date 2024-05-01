using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LeveSelectHUD : MonoBehaviour
{
    [SerializeField] private Button _nextSeasonButton;
    [SerializeField] private Button _previewSeasonButton;
    [SerializeField] private Text _seasonName;
    [SerializeField] private Text _countStarCollectedText;
    [SerializeField] private Transform[] _seasons;
    [SerializeField] private Transform _seasonPosition;
    [SerializeField] private int _seasonNumber;

    [SerializeField] private float _offsetPositionX;
    private const int _countStarOnLevel = 3;
    private int _numberSeasonPreview;


    public void SetSeasonName(int seasonNumber) =>_seasonName.text = $"Season {seasonNumber}";

    public void SetStarCollected(int countStarCollectedAll, int countStarAll) =>_countStarCollectedText.text = $"{countStarCollectedAll}/{countStarAll * _countStarOnLevel}";

    public void SelectSeason(int numberSeason)
    {
        SetSeasonName(numberSeason);
        SetVisibleButton(numberSeason);
        Vector3 position = _seasonPosition.transform.position;
        
        if (numberSeason > _numberSeasonPreview)
        {
            position.x -= _offsetPositionX;
        }
        else
        {
            position.x += _offsetPositionX;
        }
        _seasonPosition.transform.DOMoveX(position.x, 0.05f);
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
