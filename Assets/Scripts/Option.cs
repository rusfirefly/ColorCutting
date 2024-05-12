using UnityEngine;
using UnityEngine.UI;
using YG;

public class Option : MonoBehaviour
{
    [SerializeField] public GameObject _tutorial;

    [SerializeField] private Text _playerName;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Level _level;
    [SerializeField] private Button _showOptionButton;
    [SerializeField] private SoundHandler _soundHandler;
    [SerializeField] private Text _scoreText;

    private void Start()
    {
        Auth();
        
        if (YandexGame.auth)
            _loginButton.interactable = false;
        else
            _loginButton.interactable = true;

        if (YandexGame.savesData.SeasonData != null)
            SetScore(YandexGame.savesData.SeasonData.ScoreAll);
    }

    public void ShowOptions()
    {
        SetVisibleTutorial(false);
        Auth();
    }

    public void ShowAuthDialog()
    {
        if (YandexGame.auth == false)
        {
            YandexGame.AuthDialog();
        }
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

}

