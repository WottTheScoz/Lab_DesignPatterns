using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypes : MonoBehaviour, IEnemy
{
    GameObject obj;
    int score;

    public void Type(int type) {
        if (type == 1) {
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        if (type == 2) {
            obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }
        if (type == 3) {
            obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
    }

    public void Color(int red, int green, int blue) {
        //obj.GetComponent<Renderer>.material.color = new Color(red, green, blue, 1); <- not working, getcomponent throwing fit
    }

    public void Score(int s) {
        score = s;
    }

    public void Movement() {
        //uhh idk what to do here yet
    }

    public void Location(Vector2 position) {
        obj.transform.position = position;
    }

}
