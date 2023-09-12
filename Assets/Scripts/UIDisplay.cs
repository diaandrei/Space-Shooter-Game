using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{   
    // creating the fields for the health and health slider
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;



    // fields for the score and score keeper
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        // get the score keeper
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Start()
    {
        // set the max value of the health slider
        healthSlider.maxValue = playerHealth.GetHealth();
        // set the max value of the score text
        scoreText.text = scoreKeeper.GetScore().ToString();
        
    }

    void Update()
    {
        // update the health slider
        healthSlider.value = playerHealth.GetHealth();
        // update the score text
        scoreText.text = scoreKeeper.GetScore().ToString("0000000000000");
    }
}
