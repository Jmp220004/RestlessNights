using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public int damage;
    public int maxHP;
    public int currentHP;
    public bool dead;


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public void Heal(int healamt)
    {
        currentHP += healamt;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
