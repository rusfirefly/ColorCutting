using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] private Collected _collected;
    [SerializeField] private HudHandler _hud;
    [SerializeField] private Cutting _cutting;
    [SerializeField] private int _countFixedPoint;
    [SerializeField] private PointHandler _pointHandler;
    [SerializeField] private int _levelNumber;
    [SerializeField] int _countHole;

    private bool _isComplete;
    private int _holeComplete;

    public void Initialized(int maxPoint)
    {
        _hud.SetLavelNunber(_levelNumber);
        _pointHandler.Initialized(maxPoint - _countFixedPoint);
    }

    private void OnEnable()
    {
        Collected.Complete += OnComplete;
        _cutting.Lose += OnLose;
        _cutting.Cut += OnCut;
        _pointHandler.NullPoint += OnNullPoint;
    }

    private void OnDisable()
    {
        Collected.Complete -= OnComplete;
        _cutting.Lose -= OnLose;
        _cutting.Cut -= OnCut;
        _pointHandler.NullPoint -= OnNullPoint;
    }

    private void OnNullPoint()
    {
        if(_isComplete == false)
        {
            _holeComplete = 0;
            Reload();
        }
    }

    private void OnCut(int countCut)
    {
        _hud.SetCutText(countCut);
    }

    private void OnLose()
    {
        Reload();
    }

    private void OnComplete()
    {
        _holeComplete++;
        if (_holeComplete == _countHole)
        {
            _isComplete = true;
            _cutting.Complete();
            _hud.ShowCompleteLevel();
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
    
}
