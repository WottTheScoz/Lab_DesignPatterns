using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadInput : MonoBehaviour
{
    string jsonName = "EnemyData.json";
    string binName = "PlayerData.bin";

    public delegate void SaveDelegate();
    public event SaveDelegate OnLoad;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("s"))
        {
            SavingService.SaveGame(jsonName);
            SavingService.SaveGameBin(binName);
        }

        if(Input.GetKeyDown("l"))
        {
            SavingService.LoadGame(jsonName);
            SavingService.LoadGameBin(binName);
            OnLoad?.Invoke();
        }
    }
}
