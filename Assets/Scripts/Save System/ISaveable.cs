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

// ISaveable for bin information
public interface ISaveableBin
{
    string SaveID { get; }
    ScoreData ScoreData { get; }
    void LoadFromData(ScoreData data);
}

// Stores player and enemy save data; written into a Json file
[System.Serializable]
public class SaveData
{
    public string saveID;

    public Vector3 playerPosition;

    public float enemyStartTimer;
    public float enemySpeed;
    public Vector3 enemyPosition;
}

// Stores score data; written into a binary file
[System.Serializable]
public class ScoreData
{
    public string saveID;
    public int score;
}

// Contains all save and load methods
public static class SavingService
{

    // Json save game method
    public static void SaveGame(string fileName)
    {
        var result = new SaveDataContainer(); // Create a container for your data
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();

        string filePath = Path.Combine(Application.persistentDataPath, fileName);

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

        string filePath = Path.Combine(Application.persistentDataPath, fileName);

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
        var result = new ScoreData();
        
        string filePath = Path.Combine(Application.persistentDataPath, binFileName);

        BinaryFormatter formatter = new BinaryFormatter();

        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveableBin>();

        // Searches for all saveable objects and saves their data to result
        if(allSaveableObjects.Any())
        {
            foreach(var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.ScoreData;

                if (data != null)
                {
                    data.saveID = saveableObject.SaveID;
                    result = data;
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
        string filePath = Path.Combine(Application.persistentDataPath, binFileName);

        var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveableBin>();

        BinaryFormatter formatter = new BinaryFormatter();

        using(FileStream saveFile = File.Open(filePath, FileMode.Open))
        {
            ScoreData loadedData = (ScoreData) formatter.Deserialize(saveFile);

            // finds every object of type ISaveable
            foreach(var loadableObj in allLoadableObjects)
            {
                 // checks if the loadableObj and savedObjects have the same ID
                if(loadedData.saveID.Equals(loadableObj.SaveID))
                {
                    loadableObj.LoadFromData(loadedData);
                }
            }
        }
    }
}


// Creates a container class to hold all save data
[System.Serializable]
public class SaveDataContainer
{
    public List<SaveData> savedObjects = new List<SaveData>();
}
