using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Text _numberLevelText;
    [SerializeField] private StarView _starView;
    [SerializeField] private GameObject _activeLevel;
    [SerializeField] private GameObject _deactiveLevel;

    [SerializeField] private Image _complete;

    [field:SerializeField] public bool IsActive { get; private set; }
    [field:SerializeField] public int NumberLevel { get; private set; }

    [SerializeField] private Button _levelButton;

    private void Awake()
    {
        _levelButton = GetComponent<Button>();
        _starView = GetComponent<StarView>();

        SetActiveLevelVisible(IsActive);
    }
     
    public void SetStars(int countStar)
    {
        _numberLevelText.text = $"{NumberLevel}";
        for (int i = 0; i < countStar; i++)
        {
            if(i<3)            
                _starView.SetStar(i);
        }
    }

    public void Completed() => _complete.gameObject.SetActive(true);

    public void SetActiveLevelVisible(bool visible)
    {
        _levelButton.enabled = visible;
        SetDeactiveVisible(!visible);
        _activeLevel.gameObject.SetActive(visible);
    }

    private void SetDeactiveVisible(bool visible)
    {
        _deactiveLevel.gameObject.SetActive(visible);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(NumberLevel);
    }
}
