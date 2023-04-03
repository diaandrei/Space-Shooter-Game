using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    [Header("General")]
    // creating a variable for the projectile prefab
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float baseFiringRate = 0.2f;

    [Header("useAI")]
    [SerializeField] private float firingRateVariance = 0f;
    [SerializeField] private float minimumFiringRate = 0.1f;

    // creating a variable for the projectile rotation so we can rotate it as they are upside down
    [Header("Rotation")]
    [SerializeField] private Vector3 projectileRotation = Vector3.zero;


    // creating a variable so we can use the AI
    [SerializeField] private bool  useAI;

    // declared as public as i need to access it from the player script
    // Found a way how to hide it
    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
            
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {   
        // will shoot a bunch of projectiles but i need to check functionality first
        if(isFiring && firingCoroutine == null)
        // if else block updated to use a coroutine
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

IEnumerator FireContinuously()
{
    while(true)
    {
        GameObject instance = Instantiate(projectilePrefab, 
                                            transform.position, 
                                            Quaternion.Euler(projectileRotation));

        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = transform.up * projectileSpeed;
        }

        Destroy(instance, projectileLifetime);

        float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance, 
                                                baseFiringRate + firingRateVariance);

        timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

        yield return new WaitForSeconds(timeToNextProjectile);
    }
}
}