using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    EnemyTypes enemy = new EnemyTypes();

    // Start is called before the first frame update
    void Start()
    {
        enemy.Type(1);
        enemy.Score(2);
        enemy.Location(new Vector2(1, 2));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
