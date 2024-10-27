using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject EnemyObj;

    float timer;
    float maxTimer = 1f;

    EnemyBuilder[] enemyTypes = new EnemyBuilder[3];

    // Start is called before the first frame update
    void Start()
    {
        EnemyBuilder enemyBuilder;
        Shop shop = new Shop();

        enemyBuilder = new LargeEnemy();
        shop.Construct(enemyBuilder);
        enemyTypes[0] = enemyBuilder;

        enemyBuilder = new MediumEnemy();
        shop.Construct(enemyBuilder);
        enemyTypes[1] = enemyBuilder;

        enemyBuilder = new SmallEnemy();
        shop.Construct(enemyBuilder);
        enemyTypes[2] = enemyBuilder;

        timer = maxTimer;
    }

    void Update()
    {
        if(timer >= maxTimer)
        {
            GameObject newEnemy = Instantiate(EnemyObj);
            newEnemy.AddComponent<EnemyBehaviour>();

            int RNG = Random.Range(0, enemyTypes.Length);
            newEnemy.GetComponent<EnemyBehaviour>().SetValues(enemyTypes[RNG]);

            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
