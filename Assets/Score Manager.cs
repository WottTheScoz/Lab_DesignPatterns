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
        UpdateScoreText();
    }

    public void FindAllEnemies()
    {
        // Find all enemies in the scene and subscribe to their Destroyed events
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in enemies)
        {
            enemy.OnDeath += OnEnemyDestroyed;
        }
    }

    public void SetScore(int newScore)
    {
        // Update the legacy UI Text with a specific score
        score = newScore;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return score;
    }

    private void OnEnemyDestroyed(int score)
    {
        // Increase the score based on the enemy's score value
        this.score += score;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        // Update the legacy UI Text with the current score
        scoreText.text = "Score: " + score;
    }
}
