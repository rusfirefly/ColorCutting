using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private HudHandler _hud;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private PointHandler _pointHandler;
    [SerializeField] private int _levelNumber;
    [SerializeField] int _countHole;

    private int _starCollected;
    private bool _isComplete;
    private int _holeComplete;

    private int _currentSeason;
    private LevelData _levelData;
    private LevelInformation _levelInformation;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Initialized(int maxPoint)
    {
        _levelInformation = new LevelInformation();
        _levelData = LevelHandler.LoadData();
        _currentSeason = _levelData.CurrentSeason;
        _levelInformation.LevelNumber = _levelNumber;
        _levelInformation.CountStarCollected = 0;

        if (_levelData != null)
        {
            LevelInformation lavelInformation = _levelData.LevelInformation.Find(level=>level.LevelNumber == _levelNumber);
            if(lavelInformation == null)
            {
                _levelData.LevelInformation.Add(_levelInformation);
            }else
            {
                _levelInformation = lavelInformation;
            }
        }
        else
        {
            CreateNewLevelData();
        }

        _hud.SetLavelNunber(_levelNumber);
        _pointHandler.Initialized(maxPoint);
    }

    private void CreateNewLevelData()
    {
        _levelData = new LevelData();
        _levelData.LevelInformation.Add(_levelInformation);
    }

    private void OnEnable()
    {
        Collected.Complete += OnComplete;
        Star.Collected += OnCollected;
        _cutting.Lose += OnLose;
        _cutting.Cut += OnCut;
        _pointHandler.NullPoint += OnNullPoint;
    }

    private void OnDisable()
    {
        Collected.Complete -= OnComplete;
        Star.Collected -= OnCollected;
        _cutting.Lose -= OnLose;
        _cutting.Cut -= OnCut;
        _pointHandler.NullPoint -= OnNullPoint;
    }

    private void OnCollected()
    {
        _starCollected++;
        
    }

    private void OnNullPoint()
    {
        if(_isComplete == false)
        {
            _holeComplete = 0;
            _hud.SetVisibleFailLevel(true);
        }
    }

    private void OnCut(int countCut)
    {
        _hud.SetCutText(countCut);
    }

    private void OnLose()
    {
        _hud.SetVisibleFailLevel(true);
        Time.timeScale = 0;
    }

    private void OnComplete()
    {
        _holeComplete++;
        if (_holeComplete == _countHole)
        {
            _isComplete = true;
            _cutting.Complete();
            if (_starCollected > _levelInformation.CountStarCollected)
            {
                _levelInformation.CountStarCollected = _starCollected;
                _levelData.LevelInformation[_levelNumber - 1] = _levelInformation;
                _levelData.LevelInformation[_levelNumber].IsActive = true;
                Debug.Log(_levelData.LevelInformation[_levelNumber -1].LevelNumber);
                LevelHandler.SaveData(_levelData);
            }
            _hud.ShowCompleteLevel();
            _hud.SetStarCompleted(_starCollected);
        }
    }

    public void NextLevel()
    {
        _levelNumber++;
        SceneManager.LoadScene(_levelNumber);
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void AddCut(int count) =>_cutting.AddCut(count);

    public void ShowAdvertisement()
    {
        Debug.Log("�������");
        _hud.SetVisibleFailLevel(false);
        _cutting.AddCut(3);///��������
        Time.timeScale = 1;
    }
}
