using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformSaver : MonoBehaviour, ISaveable
{
    public const string SAVE_ID = "player";

    Vector3 savedPosition;

    void LoadPosition()
    {
        transform.position = savedPosition;
    }

    #region ISaveable
    public SaveData SavedData
    {
        get
        {
            var result = new SaveData();
            result.playerPosition = transform.position;
            return result;
        }
    }

    public void LoadFromData(SaveData data)
    {
        if(data.saveID.Equals(SaveID))
        {
            savedPosition = data.playerPosition;
            LoadPosition();
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
