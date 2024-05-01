using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    
    public void Play()
    {
        if (_particle.isPlaying)
            _particle.Play();
    }
}
