using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] private GameObject _thisGameObject;
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private AudioSource _deathSound;

    private RandomSpawn _randomSpawner;

    private void Awake()
    {
        _randomSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<RandomSpawn>();
    }
    public void KillObject()
    {
        PlayFX();
        if (gameObject.CompareTag("Enemy"))
        {
            _randomSpawner.AddEnemyKilled(1);
            Destroy(_thisGameObject);
        }
        else
        {
            Destroy(_thisGameObject);
        }

    }
    void PlayFX()
    {
        // play vfx
        if (_deathParticles != null)
        {
            // spawn a particle effect from assets
            ParticleSystem newParticle = Instantiate(_deathParticles,
                transform.position, Quaternion.identity);
            newParticle.Play();
        }
        // play sfx
        if (_deathSound != null)
        {
            AudioSource newSound = Instantiate(_deathSound,
                transform.position, Quaternion.identity);
            Destroy(newSound.gameObject, newSound.clip.length);
        }
    }
}
