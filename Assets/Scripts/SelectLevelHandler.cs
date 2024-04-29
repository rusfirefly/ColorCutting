using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelHandler : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private List<LevelView> _levelViews;
    [SerializeField] private LeveSelectHUD _leveSelectHUD;

    private int _seasonNumber;
    private LevelData _seasonsData;

    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        _seasonsData = LevelHandler.LoadData();
        bool isData = false;
        if(_seasonsData == null)
        {
            _seasonsData = new LevelData();
            _seasonsData.CurrentSeason = 1;

            for (int i = 0; i < _levelViews.Count; i++)
            {
                _seasonsData.LevelInformation.Add(new LevelInformation());
            }
        }
        else
        {
            isData = true;
            _seasonNumber = _seasonsData.CurrentSeason;
        }

        ViewAllLevels(isData);
    }

    public void NextSason()
    {
        _seasonNumber++;
        LoadSeason();
    }

    public void PreviwSeason()
    {
        _seasonNumber--;
        LoadSeason();
    }
    
    private void LoadSeason()
    {
        
    }

    private void ViewAllLevels(bool isData)
    {
        _leveSelectHUD.SetSeasonName(_seasonsData.CurrentSeason);
        int countStarCollectedAll = 0;
        int index = 0;

        foreach(LevelInformation levelInformation in _seasonsData.LevelInformation)
        {
            int numberLevel = isData ? levelInformation.LevelNumber : _levelViews[index].NumberLevel;
            bool activeLevel = isData ? levelInformation.IsActive : _levelViews[index].IsActive;
            int countStarCollected = isData ? levelInformation.CountStarCollected : 0;
            countStarCollectedAll += levelInformation.CountStarCollected;
            _levelViews[index].LoadLevelInformation(countStarCollected);
            _levelViews[index].SetActiveLevelVisible(activeLevel);

            if(isData == false)
            {
               levelInformation.LevelNumber = numberLevel;
               levelInformation.IsActive = activeLevel;
               levelInformation.CountStarCollected = 0;
            }

            index++;
        }

        LevelHandler.SaveData(_seasonsData);
        _leveSelectHUD.SetStarCollected(countStarCollectedAll, _seasonsData.LevelInformation.Count);
    }
}
