using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    string type;

    public float speed;
    public Color color;
    public int score;

    public Enemy(string type)
    {
        this.type = type;
    }
}
