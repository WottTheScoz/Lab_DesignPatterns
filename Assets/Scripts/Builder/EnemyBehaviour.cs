using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class EnemyBehaviour : MonoBehaviour
{
    public float maxHeight = 4;

    bool isMoving;

    float speed = 1f;
    Color color = Color.white;
    int score = 1;

    Color prevColor;

    float timer;
    float maxTimer = 10f;

    Vector3 initPosition;

    SpriteRenderer colorRenderer;

    //EnemyBuilder type;

    //public event Action<EnemyBehaviour> Destroyed;
    public delegate void EnemyDelegate(int score);
    public event EnemyDelegate OnDeath;

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        colorRenderer = GetComponent<SpriteRenderer>();

        prevColor = color;

        SetInitialPosition();

        initPosition = transform.position;
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
        if(isMoving)
        {
            Movement();
        }
    }
    #endregion

    #region Getters / Setters
    public void SetValues(EnemyBuilder enemyBuilder)
    {
        speed = enemyBuilder.enemy.speed;
        color = enemyBuilder.enemy.color;
        score = enemyBuilder.enemy.score;

        //type = enemyBuilder;
    }

    void SetColor()
    {
        colorRenderer.color = color;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public int GetScore()
    {
        return score;
    }

    public float GetSpeed()
    {
        return speed;
    }

    /*public EnemyBuilder GetEnemyBuilder()
    {
        return type;
    }*/

    public bool GetStatus()
    {
        return isMoving;
    }
    #endregion

    #region Behaviour
    void SetInitialPosition()
    {
        float RNG = Random.Range(transform.position.y, maxHeight);

        transform.position = new Vector3(transform.position.x, RNG, transform.position.z);
    }

    void ResetPosition()
    {
        transform.position = initPosition;
    }

    void Movement()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    void ExistenceTimer()
    {
        if(isMoving)
        {
            if(timer >= maxTimer)
            {
                //Destroy(this.gameObject);
                ResetPosition();
                isMoving = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            OnDeath?.Invoke(score);
            ResetPosition();
            isMoving = false;
            //Destroy(this.gameObject);
        }
    }
    #endregion
}
