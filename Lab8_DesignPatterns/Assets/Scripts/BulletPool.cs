using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    List<GameObject> bullets = new List<GameObject>();                  // the bullet pool

    #region Unity Methods
    void Start()
    {
        // adds all children to bullet pool
        for(int i = 0; i < gameObject.transform.childCount; ++i)
        {
            bullets.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
    #endregion

    #region Pool Management

    // finds next available bullet from the pool and shoots it from startPos.
    public void Shoot(Vector3 startPos)
    {   
        if(FindBullet() != null)
        {
            FindBullet().GetComponent<IBullet>().CreateWithForce(startPos);  
        } 
    }

    // returns the next available bullet from the bullet pool.
    GameObject FindBullet()
    {
        for(int i = 0; i < bullets.Count; ++i)
        {
            if(bullets[i].GetComponent<IBullet>().CheckState())
            {
                return bullets[i];
            }
        }

        return null;
    }
    #endregion
}
