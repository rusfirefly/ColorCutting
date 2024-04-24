using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Cord[] _cords;

    void Start()
    {
        foreach(Cord cord in _cords)
            cord.Initialized();    
    }

}
