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
