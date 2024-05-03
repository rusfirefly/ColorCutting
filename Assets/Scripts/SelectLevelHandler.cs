using System.Collections.Generic;
using UnityEngine;

public class SelectLevelHandler : MonoBehaviour
{
    [SerializeField] private List<LevelView> _levelViews;
    [SerializeField] private LeveSelectHUD _leveSelectHUD;

    private int _seasonNumber;
    private LevelData _seasonsData;

    private void Start()
    {
        Initialized();
        YandexSDK.LoadData();
    }

    public void Initialized()
    {
        #if UNIT_EDITOR
        _seasonsData = LevelHandler.LoadData();
        #endif
        _seasonsData = YandexSDK.LevelDataLoad;
        
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
        _seasonNumber = Mathf.Clamp(_seasonNumber, 1, 4);
       
        LoadSeason();
    }

    public void PreviwSeason()
    {
        _seasonNumber--;
        _seasonNumber = Mathf.Clamp(_seasonNumber, 1 , 4);
        
        LoadSeason();
    }
    
    private void LoadSeason()
    {
        Debug.Log(_seasonNumber);
        _leveSelectHUD.SelectSeason(_seasonNumber);
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
            bool isCompleted = isData ? levelInformation.IsCompleted : false;
            int countStarCollected = isData ? levelInformation.CountStarCollected : 0;

            countStarCollectedAll += levelInformation.CountStarCollected;

            _levelViews[index].LoadLevelInformation(countStarCollected);
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

#if UNIT_EDITOR
        LevelHandler.SaveData(_seasonsData);
#endif
        YandexSDK.SaveData(_seasonsData);
        _leveSelectHUD.SetStarCollected(countStarCollectedAll, _seasonsData.LevelInformation.Count);
        _leveSelectHUD.SetScoreText(_seasonsData.ScoreAll);
    }
}
