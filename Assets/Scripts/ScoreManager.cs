using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int highScore = 0;
    public int currentScore = 0;

    private void Awake()
    {
        int numberOfSessions = FindObjectsOfType<ScoreManager>().Length;
        if(numberOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddToScore(int score)
    {
        currentScore += score;
    }

    public void UpdateHighScore()
    {
        if(highScore < currentScore)
        {
            highScore = currentScore;
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

}
