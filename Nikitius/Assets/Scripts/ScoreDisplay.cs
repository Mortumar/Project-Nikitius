using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    Text scoreText;
    GameSession gameSession;


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
        gameSession = FindObjectOfType<GameSession>();
        scoreText.text = gameSession.GetScore().ToString();
    }
    

    
}
