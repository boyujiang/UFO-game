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
    public DateTime nextRegenTime { get; set; }
    public static GameStateController controller;

    private int regenerateLifeLatency = 3;

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
        TimeSpan timeSpan = nextRegenTime - DateTime.Now;

        if (timeSpan < TimeSpan.Zero)
        {
            if (livesRemaining < maxLives)
            {
                livesRemaining++;
            }
            nextRegenTime = DateTime.Now.AddMinutes(regenerateLifeLatency);
        }
    }

    // Used for testing purposes
    void OnGUI()
    {

    }

    // Called when the application first starts and each time it goes back into focus
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            nextRegenTime = DateTime.Now.AddMinutes(regenerateLifeLatency); // This will be overwritten if there already is a time in save file
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

        var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        state.nextRegenTime = (nextRegenTime.ToUniversalTime() - epoch).TotalSeconds;
        // TODO: add items field 

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

            var timeSpan = TimeSpan.FromSeconds(state.nextRegenTime);
            nextRegenTime = new DateTime(timeSpan.Ticks).ToLocalTime();
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
}
