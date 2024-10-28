using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Define the ISaveable interface
public interface ISaveable
{
    string SaveID { get; }
    SaveData SavedData { get; }
    void LoadFromData(SaveData data);
}


[System.Serializable]
public class SaveData
{
    public string saveID;
    public int score;
    public EnemyBuilder type;
    public Vector3 position;
}

/*
// Class that represents player save data
[System.Serializable]
public class MyData : SaveData
{
    public int score;
}

// Represents enemy save data
[System.Serializable]
public class EnemyData : SaveData
{
    public EnemyBuilder type;
    public Vector3 position;
}
*/

// Contains all save and load methods
public static class SavingService
{

    // Json save game method
    public static void SaveGame(string fileName)
    {
        var result = new SaveDataContainer(); // Create a container for your data
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        // Searches for all saveable objects and saves their data to result
        if (allSaveableObjects.Any())
        {
            foreach (var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;

                if (data != null)
                {
                    data.saveID = saveableObject.SaveID;
                    result.savedObjects.Add(data);
                }
                else
                {
                    Debug.LogWarningFormat("{0}'s save data is null. The object was not saved.", saveableObject.SaveID);
                }
            }
        }
        else
        {
            Debug.LogWarning("The scene did not include any saveable objects.");
        }

        // Saves result data to json file
        var json = JsonUtility.ToJson(result, true);
        File.WriteAllText(filePath, json);

        Debug.LogFormat("Wrote saved game to {0}", filePath);

        // Optionally, invoke garbage collection
        System.GC.Collect();
    }

    // Json load game method
    public static bool LoadGame(string fileName)
    {
        SaveDataContainer loadedData;
        var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);

        if(allLoadableObjects.Any())
        {
            // retrieves JSON data
            string dataToLoad;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            // deserializes data
            loadedData = JsonUtility.FromJson<SaveDataContainer>(dataToLoad);

            // finds every object of type ISaveable
            foreach(var loadableObj in allLoadableObjects)
            {
                // finds each set of MyData within loadedData (instance of SaveDataContainer)
                foreach(SaveData savedObjects in loadedData.savedObjects)
                {
                    // checks if the loadableObj and savedObjects have the same ID
                    if(savedObjects.saveID.Equals(loadableObj.SaveID))
                    {
                        loadableObj.LoadFromData(savedObjects);
                    }
                }
            }
        }
        System.GC.Collect();
        
        return true;
    }


    // Bin save game method
    public static void SaveGameBin(string binFileName)
    {
        var result = new SaveDataContainer(); // Create a container for your data

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), binFileName);

        BinaryFormatter formatter = new BinaryFormatter();

        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        // Searches for all saveable objects and saves their data to result
        if(allSaveableObjects.Any())
        {
            foreach(var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;

                if (data != null)
                {
                    data.saveID = saveableObject.SaveID;
                    result.savedObjects.Add(data);
                }
                else
                {
                    Debug.LogWarningFormat("{0}'s save data is null. The object was not saved.", saveableObject.SaveID);
                }
            }
        }
        else
        {
            Debug.LogWarning("The scene did not include any saveable objects.");
        }

        // saves result to a bin file
        using(FileStream saveFile = File.Create(filePath))
        {
            formatter.Serialize(saveFile, result);
        }

        System.GC.Collect();

        Debug.LogFormat("Wrote saved game to {0}", filePath);
    }

    // Bin load game method
    public static void LoadGameBin(string binFileName)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), binFileName);

        var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        BinaryFormatter formatter = new BinaryFormatter();

        using(FileStream saveFile = File.Open(filePath, FileMode.Open))
        {
            SaveDataContainer loadedData = (SaveDataContainer) formatter.Deserialize(saveFile);

            // finds every object of type ISaveable
            foreach(var loadableObj in allLoadableObjects)
            {
                // finds each set of MyData within loadedData (instance of SaveDataContainer)
                foreach(SaveData savedObjects in loadedData.savedObjects)
                {
                    // checks if the loadableObj and savedObjects have the same ID
                    if(savedObjects.saveID.Equals(loadableObj.SaveID))
                    {
                        loadableObj.LoadFromData(savedObjects);
                        Debug.Log("Got to this point");
                    }
                }
            }
        }
    }
}


// Creates a container class to hold all save data
[System.Serializable]
public class SaveDataContainer
{
    //public List<SaveData> scoreObjects = new List<SaveData>();
    //public List<SaveData> enemyObjects = new List<SaveData>();
    public List<SaveData> savedObjects = new List<SaveData>();
}
