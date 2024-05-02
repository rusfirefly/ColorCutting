using System;
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
    private int _holeComplete;

    private int _currentSeason;
    private LevelData _levelData;
    private LevelInformation _levelInformation;
    private int _score;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Initialized(int maxPoint)
    {
        _levelInformation = new LevelInformation();
        _levelData = LevelHandler.LoadData();

        _levelInformation.LevelNumber = _levelNumber;
        _levelInformation.CountStarCollected = 0;
        _levelInformation.Score = 0;

        if (_levelData != null)
        {
            _currentSeason = _levelData.CurrentSeason;
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
            _currentSeason = 1;
            CreateNewLevelData();
        }

        _hud.SetLavelNunber(_levelNumber);
        _pointHandler.Initialized(maxPoint);
    }

    private void CreateNewLevelData()
    {
        _levelData = new LevelData();
        _levelData.ScoreAll = 0;
        _levelData.LevelInformation.Add(_levelInformation);
    }

    private void OnEnable()
    {
        Star.Collected += OnCollected;
        Collected.ScoreAdd += OnScoreAdd;
        _cutting.Lose += OnLose;
        _cutting.Cut += OnCut;
        _pointHandler.NullPoint += OnNullPoint;
    }

    private void OnDisable()
    {
        Star.Collected -= OnCollected;
        Collected.ScoreAdd -= OnScoreAdd;
        _cutting.Lose -= OnLose;
        _cutting.Cut -= OnCut;
        _pointHandler.NullPoint -= OnNullPoint;
    }

    private void OnScoreAdd(int scrore)
    {
        _score = scrore;
    }

    private void OnCollected()
    {
        _starCollected++;
    }

    private void OnNullPoint()
    {
        OnComplete();
        _hud.ShowCompleteLevel();
        _holeComplete = 0;
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
            _cutting.Complete();
            _levelData.ScoreAll += _score;
            _levelData.LevelInformation[_levelNumber - 1].Score = _score;
            if (_starCollected > _levelInformation.CountStarCollected)
            {
                _levelInformation.CountStarCollected = _starCollected;
                _levelData.LevelInformation[_levelNumber - 1] = _levelInformation;
                _levelData.LevelInformation[_levelNumber].IsActive = true;
                Debug.Log(_levelData.LevelInformation[_levelNumber - 1].LevelNumber);
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
        Debug.Log("реклама");
        _hud.SetVisibleFailLevel(false);
        _cutting.AddCut(3);///временно
        Time.timeScale = 1;
    }
}
