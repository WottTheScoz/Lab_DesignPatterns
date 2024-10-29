using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    public const string SAVE_ID = "enemy position";

    float savedTimer;
    float savedSpeed;
    Vector3 savedPosition;

    EnemyBehaviour enemyBehaviour;

    void Start()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        savedSpeed = enemyBehaviour.GetSpeed();
        savedTimer = enemyBehaviour.GetTimer();
    }

    void LoadEnemies()
    {
        transform.position = savedPosition;
        enemyBehaviour.SetSpeed(savedSpeed);
        enemyBehaviour.SetTimer(savedTimer);
    }

    public void AddToSaveKey(int enemyNum)
    {
        _saveID += " " + enemyNum;
    }

    #region ISaveable
    public SaveData SavedData
    {
        get
        {
            var result = new SaveData();
            result.enemyStartTimer = savedTimer;
            result.enemySpeed = savedSpeed;
            result.enemyPosition = transform.position;
            return result;
        }
    }

    public void LoadFromData(SaveData data)
    {
        if(data.saveID.Equals(SaveID))
        {
            savedTimer = data.enemyStartTimer;
            savedSpeed = data.enemySpeed;
            savedPosition = data.enemyPosition;
            LoadEnemies();
        }
    }

    string _saveID = SAVE_ID;
    public string SaveID
    {
        get{return _saveID;}
        set{_saveID = value;}
    }

    public void OnBeforeSerialize()
    {
        if(_saveID == null)
        {
            _saveID = System.Guid.NewGuid().ToString();
        }
    }

    public void OnAfterDeserialize(){}
    #endregion
}
