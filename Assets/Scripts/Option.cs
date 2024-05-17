using UnityEngine;
using UnityEngine.UI;
using YG;
using DG.Tweening;

public class Option : MonoBehaviour
{
    [SerializeField] public GameObject _tutorial;

    [SerializeField] private Text _playerName;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Level _level;
    [SerializeField] private Button _showOptionButton;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private Text _scoreText;
    [SerializeField] private GameObject _authDialog;

    private void Start()
    {
        if (YandexGame.auth)
            _loginButton.interactable = false;
        else
            _loginButton.interactable = true;

        Auth();
        ShowScore();
    }

    public void ShowOptions()
    {
        SetVisibleTutorial(false);
        Auth();
        ShowScore();
    }

    public void ShowYandexAuthDialog()
    {
        if (YandexGame.auth == false)
        {
            YandexGame.AuthDialog();
        }
    }

    public void ShowAuthDialog()
    {
        AnimationDialog(1, 0.3f);
    }

    public void HideAuthDialog()
    {
        AnimationDialog(0, 0.3f);
    }

    private void AnimationDialog(float endValue, float duration)
    {
        _authDialog.transform.DOScale(endValue, duration);
    }

    public void Auth()
    {
        if (YandexGame.auth)
        {
            _playerName.text = YandexGame.playerName;
        }
        else
        {
            _playerName.text = "Не авторизованн";
        }
    }

    public void SetVisibleTutorial(bool visible)
    {
        if (_tutorial)
            _tutorial.gameObject.SetActive(visible);
    }

    public void Resume()
    {
        _showOptionButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Reload()
    {
        _level.Reload();
    }

    public void LoadSelectLevelScene()
    {
        _level.LoadSceneSelected();
    }

    public void SoundOnOff()
    {
        _soundHandler.SoundOnOff();
    }

    public void MusicOnOff()
    {
        _soundHandler.MusicOnOff();
    }

    private void SetScore(int score) => _scoreText.text = $"Счёт: {score}";

    private void ShowScore()
    {
        if (YandexGame.savesData.SeasonData != null)
            SetScore(YandexGame.savesData.SeasonData.ScoreAll);
    }
}

