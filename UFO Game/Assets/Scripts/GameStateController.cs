using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public bool completedTutorial;
    // public BuyableObject[] items;
    public int livesRemaining;
    public int maxLives { get; set; }
    public int maxHealth { get; set; }
    public int technology { get; set; }
    public int[] unlockedLevels { get; set; }
    public int[] allLevels { get; set; }

    public DateTime nextRegenTime { get; set; }
    public static GameStateController controller;

    private const int RegenerateLifeLatency = 5; // in minutes
    private const int Levels = 3;

    void Awake()
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
            
        }
        else if (controller != this)
        {
            Destroy(gameObject); // Enforces only one instance of GameStateController
        }

    }

    // Update is called once per frame
    void Update()
    {
        //TimeSpan timeSpan = nextRegenTime - DateTime.Now;

        if (DateTime.Compare(DateTime.Now, nextRegenTime) >= 0)
        {
            if (livesRemaining < maxLives)
            {
                
                livesRemaining++;
                Debug.Log("Life increased; Current amount of lives: " + livesRemaining);
            }
            
            nextRegenTime = DateTime.Now.AddMinutes(RegenerateLifeLatency);
        }
    }

    // Used for testing purposes
    void OnGUI()
    {
        /*
        if (GUI.Button(new Rect(10, 100, 300, 30), "Next regen: " + nextRegenTime.ToLocalTime()))
        {

        }
        */

    }

    // Called when the application first starts and each time it goes back into focus
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Debug.Log("In focus");
            allLevels = new int[Levels];
            unlockedLevels = new int[Levels];
            for (int i = 0; i < allLevels.Length; i++)
            {
                allLevels[i] = 0;
                unlockedLevels[i] = 0;
            }
            nextRegenTime = DateTime.Now.AddMinutes(RegenerateLifeLatency); // This will be overwritten if there already is a time in save file
            Load();
        }
    }

    // Called when the user leaves the game (ex. switching apps) 
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
        
    }

    // Called when the user completely exits the game
    void OnApplicationQuit()
    {
        Save();
    }

    // Serializes current game data to a file
    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameState.dat");

        GameState state = new GameState();
        state.completedTutorial = completedTutorial;
        state.technology = technology;
        state.maxLives = maxLives;
        state.maxHealth = maxHealth;
        state.livesRemaining = livesRemaining;

        for (int i = 0; i < allLevels.Length; i++)
        {
            state.allLevels[i] = allLevels[i];
            state.unlockedLevels[i] = unlockedLevels[i];
        }

        // TODO: add items field 

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        state.nextRegenTime = (nextRegenTime.ToUniversalTime() - epoch).TotalSeconds;
        Debug.Log("Save date" + nextRegenTime);
        binaryFormatter.Serialize(file, state);
        file.Close();
    }

    // Reloads game data from file into memory
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameState.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameState.dat", FileMode.Open);
            GameState state = (GameState)binaryFormatter.Deserialize(file);
            file.Close();

            completedTutorial = state.completedTutorial;
            technology = state.technology;
            maxLives = state.maxLives;
            maxHealth = state.maxHealth;
            livesRemaining = state.livesRemaining;

            for (int i = 0; i < allLevels.Length; i++)
            {
                allLevels[i] = state.allLevels[i];
                unlockedLevels[i] = state.unlockedLevels[i];
            }

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //var timeSpan = TimeSpan.FromSeconds(state.nextRegenTime);
            nextRegenTime = epoch.AddSeconds(state.nextRegenTime).ToLocalTime(); //new DateTime(timeSpan.Ticks).ToLocalTime();
            Debug.Log("Load date" + nextRegenTime);
        }
    }
}

[Serializable]
class GameState
{
    public bool completedTutorial { get; set; }
    // public BuyableObject[] items;
    public int livesRemaining { get; set; }
    public int maxLives { get; set; }
    public int maxHealth { get; set; }
    public int technology { get; set; }
    public double nextRegenTime { get; set; }
    public int[] allLevels = new int[3];
    public int[] unlockedLevels = new int[3];
}
