using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    void AddForce();                                // adds a movement force to bullet

    void RemoveForce();                             // stops bullet's movement completely

    void CreateWithForce(Vector3 startPos);         // moves bullet to startPos, then adds a movement force

    bool CheckState();                              // checks if bullet is available to shoot
}
