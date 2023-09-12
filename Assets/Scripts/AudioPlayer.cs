using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{ 
    
    [Header("Shooting")]
    // This is the sound effect that will be played when the player shoots
    [SerializeField] AudioClip shootingClip;
    // This is the volume of the shooting sound effect setted by default to 1
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;

    public AudioPlayer GetInstance()
    {
        return instance;
    }

    void Awake()
    {
       ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {   
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }


// This method is called from the Player script and plays the shooting sound effect
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    // repeating play clip for shooting and damage so we created a method that will play any clip
    void PlayClip(AudioClip clip, float volume)
    {
        if(clip != null)
        {   
            // repeating the camera position so we cast it in a variable.
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, 
                                        cameraPos, 
                                        volume);
        }
    }
}
