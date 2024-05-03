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
    private int _pointCount;
    private int _maxPoint;
    private int _currentScore;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Initialized(int maxPoint)
    {
        _currentScore = 0;
        _maxPoint = maxPoint;
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
                _currentScore = _levelData.ScoreAll - _levelInformation.Score;
            }
        }
        else
        {
            _currentSeason = 1;
            CreateNewLevelData();
        }

        _hud.SetLavelNumber(_levelNumber);
        _pointHandler.Initialized(maxPoint);
        _hud.SetScoreText(_currentScore);
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
        _currentScore += _score;
        _hud.SetScoreText(_currentScore);
        _pointCount++;
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
            bool isNewScore = false;
            bool isNewStarCollected = false;
            int scoreOld = _levelData.LevelInformation[_levelNumber - 1].Score;
            if (_score > scoreOld)
            {
                _levelData.ScoreAll -= scoreOld;
                _levelData.ScoreAll += _score;
                _levelData.LevelInformation[_levelNumber - 1].Score = _score;

                if (_pointCount == _maxPoint)
                {
                    _levelData.LevelInformation[_levelNumber - 1].IsCompleted = true;
                }

                isNewScore = true;
            }

            if (_starCollected > _levelInformation.CountStarCollected)
            {
                _levelInformation.CountStarCollected = _starCollected;
                _levelData.LevelInformation[_levelNumber - 1] = _levelInformation;
                isNewStarCollected = true;
            }

            
            if (isNewScore || isNewStarCollected)
            {
                _levelData.LevelInformation[_levelNumber].IsActive = true;
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
