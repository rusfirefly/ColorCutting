using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public static event Action Destroed;
    [SerializeField] private ParticleSystem _boomEffect;

    [field: SerializeField] public Cord Cord { get; set; }

    private bool _isCollider;
    private bool _isDestroy;

    private MeshRenderer _meshRender;

    private void Start()
    {
        _meshRender = GetComponent<MeshRenderer>();
   }

    private void OnTriggerEnter(Collider other)
    {
        if (_isCollider) return;

        if(other.tag == "Ground")
        {
            Destroy();
        }
    }

    public void StartBoomEffect()
    {
        if(_boomEffect)
        {
            if (_boomEffect.isPlaying)
            {
                _boomEffect.Play();
            }
        }
    }

    public void CollectedEffect()
    {

    }

    public void Destroy()
    {
        if (_isDestroy == false)
        {
            Destroed?.Invoke();
            _isCollider = true;

            StartBoomEffect();
            Destroy(gameObject, 0.1f);
            _isDestroy = true;
        }
    }
}
