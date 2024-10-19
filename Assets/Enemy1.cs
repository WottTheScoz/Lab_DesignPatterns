using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Enemy1 : MonoBehaviour
{
    public event Action<Enemy1> Destroyed; // Pass the enemy instance
    EnemyTypes enemy = new EnemyTypes();

    private int score; 

    void Start()
    {
        enemy.Type(1);
        score = 2; 
        enemy.Score(score);
        enemy.Location(new Vector2(1, 2));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroyed?.Invoke(this);

            Destroy(gameObject);
        }
    }

    public int GetScore()
    {
        return score; 
    }
}
