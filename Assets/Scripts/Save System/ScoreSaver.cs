using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaver : MonoBehaviour, ISaveableBin
{
    public const string SAVE_ID = "score";

    int savedScore;

    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GetComponent<ScoreManager>();
    }

    void LoadScore()
    {
        scoreManager.SetScore(savedScore);
    }

    #region ISaveable
    public ScoreData ScoreData
    {
        get
        {
            var result = new ScoreData();
            result.score = scoreManager.GetScore();
            return result;
        }
    }

    public void LoadFromData(ScoreData data)
    {
        if(data.saveID.Equals(SAVE_ID))
        {
            savedScore = data.score;
            LoadScore();
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
