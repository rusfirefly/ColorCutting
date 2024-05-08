using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
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
    private SavesYG _saveYG;
    private LevelInformation _levelInformation;
    private int _score;
    private int _pointCount;
    private int _maxPoint;
    private int _currentScore;

    private void Awake()
    {
        YandexGame.LoadProgress();
    }

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Initialized(int maxPoint)
    {
        _currentScore = 0;
        _maxPoint = maxPoint;
        _levelData = YandexGame.savesData.SeasonData;
        
        if (_levelData != null)
        {
            _currentSeason = _levelData.CurrentSeason;
            LevelInformation lavelInformation = _levelData.LevelInformation.Find(level=>level.LevelNumber == _levelNumber);
            if(lavelInformation == null)
            {
                _levelInformation.CountStarCollected = 0;
                _levelInformation.IsActive = true;
                _levelInformation.IsCompleted = false;
                _levelInformation.Score = 0;

                _levelData.LevelInformation.Add(_levelInformation);
            }
            else
            {
                _levelInformation = lavelInformation;
                _currentScore = _levelData.ScoreAll - _levelInformation.Score;
            }
            
        }
 
        _hud.SetLavelNumber(_levelNumber);
        _pointHandler.Initialized(maxPoint);
        _hud.SetScoreText(_currentScore);
    }

    private void OnEnable()
    {
        Star.Collected += OnCollected;
        Collected.ScoreAdd += OnScoreAdd;
        _cutting.Lose += OnLose;
        _cutting.Cut += OnCut;
        _pointHandler.NullPoint += OnNullPoint;

        YandexGame.RewardVideoEvent += OnReward;
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        Star.Collected -= OnCollected;
        Collected.ScoreAdd -= OnScoreAdd;
        _cutting.Lose -= OnLose;
        _cutting.Cut -= OnCut;
        _pointHandler.NullPoint -= OnNullPoint;

        YandexGame.RewardVideoEvent -= OnReward;
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void GetLoad()
    {
        _levelData = YandexGame.savesData.SeasonData;   
    }

    private void OnReward(int id)
    {
        if(id == 3)
            _cutting.AddCut(id);
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

            if (_starCollected >= _levelInformation.CountStarCollected)
            {
                _levelInformation.CountStarCollected = _starCollected;
                _levelData.LevelInformation[_levelNumber - 1] = _levelInformation;
                isNewStarCollected = true;
            }

            
            if (isNewScore || isNewStarCollected)
            {
                _levelData.LevelInformation[_levelNumber].IsActive = true;
            }

            _hud.ShowCompleteLevel();
            _hud.SetStarCompleted(_starCollected);
        }
    }

    public void NextLevel()
    {
        YandexGame.savesData.SeasonData = _levelData;
        YandexGame.SaveProgress();

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
         YandexGame.RewVideoShow(3);
        _hud.SetVisibleFailLevel(false);
    }


}
