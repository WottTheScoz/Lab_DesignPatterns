using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float maxHeight = 4;

    float speed = 1f;
    Color color = Color.white;
    int score = 1;

    Color prevColor;

    float timer;
    float maxTimer = 10f;

    SpriteRenderer colorRenderer;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        colorRenderer = GetComponent<SpriteRenderer>();

        prevColor = color;

        SetInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if(colorRenderer.color != prevColor)
        {
            SetColor();
            prevColor = color;
        }

        ExistenceTimer();
    }

    void FixedUpdate()
    {
        Movement();
    }
    #endregion

    #region Getters / Setters
    public void SetValues(EnemyBuilder enemyBuilder)
    {
        speed = enemyBuilder.enemy.speed;
        color = enemyBuilder.enemy.color;
        score = enemyBuilder.enemy.score;
    }

    void SetColor()
    {
        colorRenderer.color = color;
    }

    int GetScore()
    {
        return score;
    }
    #endregion

    #region Behaviour
    void SetInitialPosition()
    {
        float RNG = Random.Range(transform.position.y, maxHeight);

        transform.position = new Vector3(transform.position.x, RNG, transform.position.z);
    }

    void Movement()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    void ExistenceTimer()
    {
        if(timer >= maxTimer)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
