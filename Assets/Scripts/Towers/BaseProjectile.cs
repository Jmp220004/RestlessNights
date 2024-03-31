using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _lifeSpan;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public virtual void Move()
    {

    }

    void SelfDestruct() //destroys self if collides with enemy/environment/reaches lifespan time
    {
        
    }
}
