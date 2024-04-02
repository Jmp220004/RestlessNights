using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour


{
    // Start is called before the first frame update
    [SerializeField] public GameObject Enemy;
    [SerializeField] public Health health;
    void Start()
    {
        if (health != null)
        {
            Debug.Log(health.currentHP);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // Input player health code here
            Destroy(other.gameObject);
            Debug.Log("Lost Health");
            health.TakeDamage(Enemy.GetComponent<Health>().damage);
            Debug.Log(health.currentHP);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}