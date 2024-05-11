using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SelectLevelHandler : MonoBehaviour
{
    [SerializeField] private List<LevelView> _levelViews;
    [SerializeField] private LeveSelectHUD _leveSelectHUD;
    [SerializeField] private Text _data;

    private int _seasonNumber;
    private LevelData _seasonsData;
   
    private bool IsNewSeason => _levelViews.Count > _seasonsData.LevelInformation.Count;

    private void Awake()
    {
        YandexGame.LoadProgress();
        _leveSelectHUD = GetComponent<LeveSelectHUD>();
    }

    private void Start()
    {
        _seasonNumber = 1;
        Initialized();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;
    }

    private void GetLoad()
    {
        _seasonsData = YandexGame.savesData.SeasonData;
    }

    public void Initialized()
    {
        _seasonsData = YandexGame.savesData.SeasonData;

        bool isData = false;
        if(_seasonsData == null)
        {
            SaveNewSeason();
        }
        else
        {
            isData = true;
            //если новые сезоны. сделать проверку и сохранить.
            if (IsNewSeason)
            {
                Debug.Log("ok");
                AddNewSeason();
                SaveData();
            }
            _seasonNumber = _seasonsData.CurrentSeason;
        }

        ViewAllLevels(isData);
    }

    public void NextSason()
    {
        _seasonNumber++;
        _seasonNumber = Mathf.Clamp(_seasonNumber, 1, 4);
       
        LoadSeason();
    }

    public void PreviwSeason()
    {
        _seasonNumber--;
        _seasonNumber = Mathf.Clamp(_seasonNumber, 1 , 4);
        
        LoadSeason();
    }
    
    public void LoadSeason()
    {
        _leveSelectHUD.SelectSeason(_seasonNumber);
    }

    private void SaveNewSeason()
    {
        _seasonsData = new LevelData();
        _seasonsData.CurrentSeason = 1;
        _seasonsData.ScoreAll = 0;

        for (int i = 0; i < _levelViews.Count; i++)
        {
            LevelInformation info = new LevelInformation();
            info.LevelNumber = _levelViews[i].NumberLevel;
            info.IsActive = _levelViews[i].IsActive;
            info.IsCompleted = false;
            info.Score = 0;
            _seasonsData.LevelInformation.Add(info);
        }
        SaveData();
    }

    private void SaveData()
    {
        YandexGame.savesData.SeasonData = _seasonsData;
        YandexGame.SaveProgress();
    }

    private void AddNewSeason()
    {
        for (int i = _seasonsData.LevelInformation.Count; i < _levelViews.Count; i++)
        {
           LevelInformation info = new LevelInformation();
           info.LevelNumber = _levelViews[i].NumberLevel;
           info.IsActive = _levelViews[i].IsActive;
           info.IsCompleted = false;
           info.Score = 0;
           _seasonsData.LevelInformation.Add(info);
        }
    }

    private void ViewAllLevels(bool isData)
    {
        _leveSelectHUD.SetSeasonName(_seasonsData.CurrentSeason);
        int countStarCollectedAll = 0;
        int index = 0;
        int score = _seasonsData.ScoreAll;
        _leveSelectHUD.SetScoreText(score);

        foreach (LevelInformation levelInformation in _seasonsData.LevelInformation)
        {
            int numberLevel = isData ? levelInformation.LevelNumber : _levelViews[index].NumberLevel;
            bool activeLevel = isData ? levelInformation.IsActive : _levelViews[index].IsActive;
            bool isCompleted = isData ? levelInformation.IsCompleted : false;
            int countStarCollected = isData ? levelInformation.CountStarCollected : 0;

            countStarCollectedAll += levelInformation.CountStarCollected;

            _levelViews[index].SetStars(countStarCollected);
            _levelViews[index].SetActiveLevelVisible(activeLevel);

            if (isCompleted)
            {
                _levelViews[index].Completed();
            }

            if (isData == false)
            {
               levelInformation.LevelNumber = numberLevel;
               levelInformation.IsActive = activeLevel;
               levelInformation.CountStarCollected = 0;
            }

            index++;
        }

        _leveSelectHUD.SetStarCollected(countStarCollectedAll, _seasonsData.LevelInformation.Count);
    }

    //для сброса . удалить в релизе!!!
    public void ResetSaveData()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

}
