using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour, IBullet
{
    public float speed = 3f;

    Vector3 initialPosition;

    Rigidbody2D rb;

    #region Unity Methods
    void Start()
    {
        initialPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
    }

    // resets state upon collision (except with player)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            ToInitialState();
        }
    }

    // resets state after going offscreen
    void OnBecameInvisible()
    {
        ToInitialState();
    }
    #endregion

    #region IBullet Methods
    public void AddForce()
    {
        rb.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
    }

    public void RemoveForce()
    {
        rb.velocity = Vector3.zero;
    }

    public void CreateWithForce(Vector3 startPos)
    {
        transform.position = startPos;
        AddForce();
    }

    public bool CheckState()
    {
        bool isAvailable = true;

        if(transform.position != initialPosition)
        {
            isAvailable = false;
        }

        return isAvailable;
    }
    #endregion

    #region State Management
    
    // resets bullet movement and position, making it available to shoot again.
    void ToInitialState()
    {
        RemoveForce();
        transform.position = initialPosition;
    }
    #endregion
}
