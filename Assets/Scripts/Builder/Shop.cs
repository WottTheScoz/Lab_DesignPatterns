using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public void Construct(EnemyBuilder enemyBuilder)
    {
        enemyBuilder.BuildSpeed();
        enemyBuilder.BuildColor();
        enemyBuilder.BuildScore();
    }
}
