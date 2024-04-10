using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed;
    void Start()
    {
        speed = speed / 100;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(-speed, 0, 0);
    }


}
