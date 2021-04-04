using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int scoreToAdd = 100;


    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        // сразу скажу, что синглтоны это очень плохо и научись ими сразу не пользоваться или пользоваться в очень отдельных случаях
        // они позволяют доступ к инфе по всему приложению, что плохо и черевато проблемами с прослеживанием операций и запутыванием кода в спагетти
        // в Unity ими любят пользоваться, но на самом деле это очень плохая привычка и на всех проектах где я их видел выходил ужасное говно в конце концов
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore()
    {
        score += scoreToAdd;
    }
   
    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
