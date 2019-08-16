using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    ParticleSystem explosion;
    
    void Start()
    {
        explosion = GetComponent<ParticleSystem>();        
    }

    void Update()
    {
        if (!explosion.isPlaying)
        {
            gameObject.SetActive(false);
            explosion.time = 0;
        }
    }
}