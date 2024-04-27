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
        _levelInformation.levelNumber = _levelNumber;
        _levelInformation.countStarCollected = 0;

        if (_levelData != null)
        {
            LevelInformation lavelInformation = _levelData.LevelInformation.Find(level=>level.levelNumber == _levelNumber);
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
            _hud.ShowCompleteLevel();
            if(_starCollected > _levelInformation.countStarCollected)
            {
                _levelInformation.countStarCollected = _starCollected;
                _levelData.LevelInformation[_levelNumber - 1] = _levelInformation;
                LevelHandler.SaveData(_levelData);
            }
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
