using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    [SerializeField] private GameObject _completeLevel;
    [SerializeField] private GameObject _failLevel;
    [SerializeField] private Text _levelName;
    [SerializeField] private Text _cutText;
    [SerializeField] private Button _reloadButton;
    [SerializeField] private StarView _starView;

    private void Start()
    {
        _starView = GetComponent<StarView>();
    }

    public void SetLavelNunber(int levelNumvber) => _levelName.text = $"Level {levelNumvber}";

    public void ShowCompleteLevel()
    {
        SetVisibleRelodButton(false);
        _completeLevel.gameObject.SetActive(true);
    }

    public void SetStarCompleted(int countStar)
    {
        for(int i = 0; i < countStar; i++)
        {
            _starView.SetStar(i);
        }
    }

    public void SetVisibleFailLevel(bool visible)
    {
        SetVisibleRelodButton(!visible);
        _failLevel.gameObject.SetActive(visible);
    }

    public void SetVisibleRelodButton(bool visible) => _reloadButton.gameObject.SetActive(visible);

    public void SetCutText(int countCut) => _cutText.text = $"{countCut}";

}
