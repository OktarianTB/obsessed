using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighScore : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreText = GetComponent<TextMeshProUGUI>();

        if (!scoreManager)
        {
            Debug.LogWarning("Score manager hasn't been found");
        }
    }

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        scoreText.text = scoreManager.highScore.ToString();
    }
}
