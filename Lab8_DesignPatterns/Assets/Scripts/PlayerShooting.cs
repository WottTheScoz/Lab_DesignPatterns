using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPoolObj;

    BulletPool bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletPool = bulletPoolObj.GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            bulletPool.Shoot(transform.position);
        }
    }
}
