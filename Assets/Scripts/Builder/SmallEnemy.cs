using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SmallEnemy : EnemyBuilder
{
    public SmallEnemy()
    {
        _enemy = new Enemy("Small Enemy");
    }

    public override void BuildSpeed()
    {
        _enemy.speed = 7f;
    }

    public override void BuildColor()
    {
        _enemy.color = Color.red;
    }

    public override void BuildScore()
    {
        _enemy.score = 3;
    }
}
