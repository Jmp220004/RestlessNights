using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class healthNew : MonoBehaviour
{
    [Header("Health Values")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float startingHealth;
    [SerializeField] private float maxHealth;
    [Space]
    [Header("Event Options")]
    [SerializeField] private UnityEvent onCreate;
    [SerializeField] private UnityEvent onDie;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onHeal;
    [Space]
    [Header("Settings")]
    [Range(0f, 3f)]
    [SerializeField] private float damageResistance = 1.0f;
    [SerializeField] public bool isInvincible = false;

    private void Start()
    {
        initHealth();
    }

    public float getHealth()
    {
        return currentHealth;
    }

    public float getHealthRatio()
    {
        float healthRatio = currentHealth / maxHealth;
        return healthRatio;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public void setDamageResistance(float newDamageResistance)
    {
        damageResistance = newDamageResistance;
        if(damageResistance > 3f)
        {
            damageResistance = 3f;
        }
        else if(damageResistance < 0f)
        {
            damageResistance = 0f;
        }
    }

    public void dealDamage(float incomingDamage)
    {
        if(!isInvincible)
        {
            currentHealth -= incomingDamage * damageResistance;

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                die();
            }

            onDamage.Invoke();
        }
    }

    public void healDamage(float healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        onHeal.Invoke();
    }

    private void initHealth()
    {
        //Set the current health to the starting health, and then ensure that value is between both the maximum and starting healths
        currentHealth = startingHealth;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if(currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " started with " + currentHealth + " and was killed instantly.");
            die();
        }

        onCreate.Invoke();
    }

    private void die()
    {
        onDie.Invoke();
    }
}
