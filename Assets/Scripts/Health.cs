using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // get the health from the enemy
    [SerializeField] int health = 50;

    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CamerShake cameraShake;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CamerShake>();
    }

// destroy the enemy when it gets hit by the damage dealer
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCmaera();
            damageDealer.Hit();
        }
    }

// update the health of the enemy
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

// attaching the explosion effect to the ships
    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCmaera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
          
    }
}
