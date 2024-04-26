using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Cord[] _cords;
    [SerializeField] private Level _level;

    void Start()
    {
        int maxPoint = 0;
        _cords = FindObjectsOfType<Cord>();

        foreach (Cord cord in _cords)
        {
            cord.Initialized();
            maxPoint += cord.CountPoint;
        }
        _level.Initialized(maxPoint);
    }

}
