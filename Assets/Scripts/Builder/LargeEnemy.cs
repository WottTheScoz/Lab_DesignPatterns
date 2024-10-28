using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LargeEnemy : EnemyBuilder
{
    public LargeEnemy()
    {
        _enemy = new Enemy("Large Enemy");
    }

    public override void BuildSpeed()
    {
        _enemy.speed = 3f;
    }

    public override void BuildColor()
    {
        _enemy.color = Color.green;
    }

    public override void BuildScore()
    {
        _enemy.score = 1;
    }
}
