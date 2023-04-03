using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // get the damage from the damage dealer
    [SerializeField] int damage = 10;

    public int GetDamage()
    {
        return damage;
    }

// destroy the damage dealer when it hits the enemy
    public void Hit()
    {
        Destroy(gameObject);
    }
}
