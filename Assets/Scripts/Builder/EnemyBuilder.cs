using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyBuilder
{
    protected Enemy _enemy;
    public Enemy enemy
    {
        get{return _enemy;}
    }

    public abstract void BuildSpeed();
    public abstract void BuildColor();
    public abstract void BuildScore();
}
