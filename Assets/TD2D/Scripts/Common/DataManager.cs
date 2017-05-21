using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Version of saved data format. Use it to check if stored data format is equal to actual data format
/// </summary>
[Serializable]
public class DataVersion
{
    public int major;
    public int minor;
}

/// <summary>
/// Format of stored game progress data.
/// </summary>
[Serializable]
public class GameProgressData
{
    public System.DateTime saveTime;						// Saving time
    public string lastCompetedLevel;						// Name of level was last completed
	public List<string> openedLevels = new List<string>();	// List with levels available to play
}

/// <summary>
/// Saving and load data from file.
/// </summary>
public class DataManager : MonoBehaviour
{
	// Game progress data container
    public GameProgressData progress = new GameProgressData();
	// Singleton
    public static DataManager instance;

	// Name of file with data version
    private string dataVersionFile = "/DataVersion.dat";
	// Name of file with game progress data
    private string gameProgressFile = "/GameProgress.dat";
	// Data version container
    private DataVersion dataVersion = new DataVersion();
	// Default game progress data container
    private GameProgressData gameProgressDefaultData = new GameProgressData();

	/// <summary>
	/// Awake this instance.
	/// </summary>
    void Awake()
    {
        if (instance == null)
        {
            // Data format version
            dataVersion.major = 1;
            dataVersion.minor = 0;

            // Defalt game progress data
            progress.saveTime = gameProgressDefaultData.saveTime = DateTime.MinValue;
            progress.lastCompetedLevel = gameProgressDefaultData.lastCompetedLevel = "";

            instance = this;
            DontDestroyOnLoad(gameObject);

			//DeleteGameProgress(); // For debugging. Uncomment this lines to delete saved game progress
			//Debug.Log("Saved game progress deleted");

            UpdateDataVersion();
            LoadGameProgress();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

	/// <summary>
	/// Updates the version of data format.
	/// </summary>
    private void UpdateDataVersion()
    {
        if (File.Exists(Application.persistentDataPath + dataVersionFile) == true)
        {
            BinaryFormatter bfOpen = new BinaryFormatter();
            FileStream fileToOpen = File.Open(Application.persistentDataPath + dataVersionFile, FileMode.Open);
            DataVersion version = (DataVersion)bfOpen.Deserialize(fileToOpen);
            fileToOpen.Close();

            switch (version.major)
            {
                case 1:
					// Stored data has version 1.x
					// Some handler to convert data if it is needed ...
                    break;
            }
        }
        BinaryFormatter bfCreate = new BinaryFormatter();
        FileStream fileToCreate = File.Create(Application.persistentDataPath + dataVersionFile);
        bfCreate.Serialize(fileToCreate, dataVersion);
        fileToCreate.Close();
    }

	/// <summary>
	/// Delete file with saved game data. For debug only
	/// </summary>
	private void DeleteGameProgress()
	{
		File.Delete(Application.persistentDataPath + gameProgressFile);
	}

	/// <summary>
	/// Saves the game progress to file.
	/// </summary>
    public void SaveGameProgress()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + gameProgressFile);
        progress.saveTime = DateTime.Now;
        bf.Serialize(file, progress);
        file.Close();
    }

	/// <summary>
	/// Loads the game progress from file.
	/// </summary>
    public void LoadGameProgress()
    {
        if (File.Exists(Application.persistentDataPath + gameProgressFile) == true)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + gameProgressFile, FileMode.Open);
            progress = (GameProgressData)bf.Deserialize(file);
            file.Close();
        }
    }
}
