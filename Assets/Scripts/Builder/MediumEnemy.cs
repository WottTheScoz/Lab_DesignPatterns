using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MediumEnemy : EnemyBuilder
{
    public MediumEnemy()
    {
        _enemy = new Enemy("Medium Enemy");
    }

    public override void BuildSpeed()
    {
        _enemy.speed = 5f;
    }

    public override void BuildColor()
    {
        _enemy.color = Color.blue;
    }

    public override void BuildScore()
    {
        _enemy.score = 2;
    }
}
