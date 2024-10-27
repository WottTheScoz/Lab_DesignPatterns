using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; 
    private int score = 0;

    void Start()
    {
        // Find all enemies in the scene and subscribe to their Destroyed events
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.Destroyed += OnEnemyDestroyed;
        }

        UpdateScoreText();
    }

    private void OnEnemyDestroyed(EnemyBehaviour enemy)
    {
        // Increase the score based on the enemy's score value
        score += enemy.GetScore();
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Update the legacy UI Text with the current score
        scoreText.text = "Score: " + score;
    }
}
