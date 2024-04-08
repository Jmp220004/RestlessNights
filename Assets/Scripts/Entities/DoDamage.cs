using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [SerializeField] int _damage;

    //private GameObject _objectToDamage;

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision obj)
    {
        healthNew _currentHealth = obj.gameObject.GetComponent<healthNew>();


            if (_currentHealth != null)
            {
                _currentHealth.dealDamage(_damage);
            }
        
    }
}
