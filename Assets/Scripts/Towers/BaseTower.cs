using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] private int _chargeNeededToFire = 1;
    private int _currentCharge;
    private bool _isCharged = false;

    [Header("")]
    [SerializeField] private GameObject _protectileToFire;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private float _projectileSpeed;

    [Header ("TowerFX")]
    [SerializeField] private ParticleSystem _shootVFX;
    [SerializeField] private AudioSource _shootSound;
    // Start is called before the first frame update
    void Start()
    {
        _currentCharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        VerifyCharge();
        Shoot();
    }

    void VerifyCharge()
    {
        if (_currentCharge >= _chargeNeededToFire)
        {
            _isCharged = true;
        }
    }

    public virtual void Shoot()    //instantiate the projectile in direction
    {
        if(_isCharged == true)
        {
            PlayFX();
            GameObject bulletObj = Instantiate(_protectileToFire, _projectileSpawnPoint.transform.position,
    _projectileSpawnPoint.transform.rotation) as GameObject;
          
            //reduce current charge held by tower, by the ammount of charge it needs to fire
            _currentCharge = _currentCharge - _chargeNeededToFire;
            _isCharged = false;
        }
        /*else
        {
            Debug.Log("not enough charge to fire projectile");
        }*/
    }

    void PlayFX()
    {
        if (_shootVFX != null)
        {
            // spawn a particle effect from assets
            ParticleSystem newParticle = Instantiate(_shootVFX,
                transform.position, Quaternion.identity);
            newParticle.Play();
        }
        // play sfx
        if (_shootSound != null)
        {
            AudioSource newSound = Instantiate(_shootSound,
                transform.position, Quaternion.identity);
            Destroy(newSound.gameObject, newSound.clip.length);
        }
    }

    public void AddCharge(int chargeToAdd)
    {
        _currentCharge += chargeToAdd;
    }
}
