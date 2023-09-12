using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    int score;

    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void Start()
    {
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void ManageSingleton()
    {
        if (instance != null)
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

    // score getter
    public int GetScore()
    {
        return score;
    }

    // method for modifying the score
    public void ModifyScore(int value)
    {
        score += value;
        // clamping the value so it doesn't go below 0
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    // method for resetting the score
    public void ResetScore()
    {
        score = 0;
    }

    // Add this new method
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            ResetScore();
        }
    }
}
