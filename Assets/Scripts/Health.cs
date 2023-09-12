using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    // check if the object is a player
    [SerializeField] bool isPlayer;
    // get the health from the enemy and the player
    [SerializeField] bool isBoss;
    // get the health from the enemy and the player
    
    [SerializeField] int health = 50;

    [SerializeField] int score = 50;

    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CamerShake cameraShake;

    // reference to the audio player
    AudioPlayer audioPlayer;

    // reference to the score keeper
    ScoreKeeper scoreKeeper;

    // adding the level manager to game scene.
    LevelManager levelManager;

    void Awake()
    {   
            // get the camera shake from the main camera 
        cameraShake = Camera.main.GetComponent<CamerShake>();
        // get the audio player
        audioPlayer = FindObjectOfType<AudioPlayer>();
        // get the score keeper
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        // get the level manager
        levelManager = FindObjectOfType<LevelManager>();
    }

// destroy the enemy when it gets hit by the damage dealer
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageClip();
            ShakeCmaera();
            damageDealer.Hit();
        }
    }

    // get the health of the user
    public int GetHealth()
    {
        return health;
    }

// update the health of the enemy
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

   void Die()
    {
    if (!isPlayer)
    {
        scoreKeeper.ModifyScore(score);
    }
    // loading the game over scene when the player dies
    else
    {
        levelManager.LoadGameOver();
    }

    Destroy(gameObject);
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

    // method to shake the camera when the enemy gets hit
    void ShakeCmaera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
          
    }
    public void IncreaseHealth(int amount)
    {
        health += amount;
        //  Prevent health from going above a maximum value
        health = Mathf.Min(health, 100);  // For example, if 100 is the max health
    }
}
