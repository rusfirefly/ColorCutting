using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public static event Action Destroed;
    [SerializeField] private ParticleSystem _boomEffect;

    [field: SerializeField] public Cord Cord { get; set; }

    private bool _isCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollider) return;

        if(other.tag == "Ground")
        {
            Destroed?.Invoke();
            _isCollider = true;
            Destroy(gameObject);
        }
    }

    public void StartBoomEffect()
    {
        Debug.Log("boom!");
        if(_boomEffect)
        {
            if (_boomEffect.isPlaying == false)
                _boomEffect.Play();
        }
    }
}
