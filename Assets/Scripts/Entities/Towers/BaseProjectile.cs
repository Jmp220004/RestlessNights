using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
   // [SerializeField] private float _projectileSpeed; *currently unused. may implement later*
    [SerializeField] private float _lifeSpan;       //time to pass until object destroys itself
    [SerializeField] private float _moveDirectionX; //negative var will move object right, positive moves left
    [SerializeField] private GameObject _projectile;//projectile gameobject needed in order to destroy self

    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        Move();
        SelfDestruct();
    }

    //move projectile left to right, or right to left, based on _moveDirectionX value
    public virtual void Move()
    {
        transform.localPosition += new Vector3(_moveDirectionX, 0, 0) * Time.fixedDeltaTime;
    }

    //destroys self if collides with enemy/environment/reaches lifespan time
    public virtual void SelfDestruct() 
    {
        //add destroy on collision with enemy
        Destroy(_projectile, _lifeSpan);
    }
}
