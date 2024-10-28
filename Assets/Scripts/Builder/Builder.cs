using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public GameObject EnemyObj;
    List<GameObject> enemyList = new List<GameObject>();

    public GameObject scoreManager;

    public int maxEnemies = 20;

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

        for(int i = 0; i < maxEnemies; ++i)
        {
            CreateEnemy();
        }

        scoreManager.GetComponent<ScoreManager>().FindAllEnemies();
    }

    void Update()
    {
        if(enemyList.Count >= maxEnemies)
        {
            if(timer >= maxTimer)
            {
                for(int i = 0; i < enemyList.Count; ++i)
                {
                    if(!enemyList[i].GetComponent<EnemyBehaviour>().GetStatus())
                    {
                        enemyList[i].GetComponent<EnemyBehaviour>().StartMovement();
                        break;
                    }
                }
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        
    }

    void CreateEnemy()
    {
        GameObject newEnemy = Instantiate(EnemyObj);
        newEnemy.AddComponent<EnemyBehaviour>();

        int RNG = Random.Range(0, enemyTypes.Length);
        newEnemy.GetComponent<EnemyBehaviour>().SetValues(enemyTypes[RNG]);
        
        enemyList.Add(newEnemy);
    }

    public void CreateEnemyOfType(EnemyBuilder type, Vector3 position)
    {
        GameObject newEnemy = Instantiate(EnemyObj);
        newEnemy.AddComponent<EnemyBehaviour>();

        EnemyBuilder foundType = null;
        foreach(EnemyBuilder enemyType in enemyTypes)
        {
            if(enemyType == foundType)
            {
                foundType = enemyType;
                newEnemy.GetComponent<EnemyBehaviour>().SetValues(foundType);

                newEnemy.transform.position = position;

                enemyList.Add(newEnemy);
                break;
            }
        }
    }

    public EnemyBuilder GetRecentEnemy()
    {
        return enemyList[enemyList.Count - 1].GetComponent<EnemyBehaviour>().GetEnemyBuilder();
    }
}
