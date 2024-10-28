using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSaver : MonoBehaviour, ISaveable
{
    public const string SAVE_ID = "enemy position";

    public GameObject saveSystemObj;

    EnemyBuilder enemyType;
    Vector3 savedPosition;

    SaveLoadInput saveSystem;
    Builder builder;

    void Start()
    {
        saveSystem = saveSystemObj.GetComponent<SaveLoadInput>();
        saveSystem.OnLoad += LoadEnemies;

        builder = GetComponent<Builder>();
    }

    void LoadEnemies()
    {
        builder.CreateEnemyOfType(enemyType, savedPosition);
    }

    #region ISaveable
    public SaveData SavedData
    {
        get
        {
            var result = new SaveData();
            result.type = builder.GetRecentEnemy();
            result.position = transform.position;
            return result;
        }
    }

    public void LoadFromData(SaveData data)
    {
        if(data.saveID.Equals(SAVE_ID))
        {
            enemyType = data.type;
            savedPosition = data.position;
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
