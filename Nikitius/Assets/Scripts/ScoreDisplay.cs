using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    int score;
    Text scoreText;
    int scoreToAdd = 100;

    void Start()
    {
        scoreText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        scoreText.text = score.ToString();
    }
    
    public void AddScore()
    {
        score = score + scoreToAdd;
    }
    
}
