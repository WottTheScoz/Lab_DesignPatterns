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
        Enemy1[] enemies = FindObjectsOfType<Enemy1>();
        foreach (Enemy1 enemy in enemies)
        {
            enemy.Destroyed += OnEnemyDestroyed;
        }

        UpdateScoreText();
    }

    private void OnEnemyDestroyed(Enemy1 enemy)
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
