using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LivesDisplay : MonoBehaviour
{
    Text livesText;
    int lives = 5;
    void Start()
    {
        livesText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        livesText.text = lives.ToString();
    }

    // методы всегда с большой буквы
    public void takeLive()
    {
        lives--;
    }
}
