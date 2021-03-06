﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public bool completedTutorial {
        get {
            return _completedTutorial;
        }
        set {
            _completedTutorial = value;
            Save();
        }
    }
    public Dictionary<string, int> items { get; set; }
    public int livesRemaining {
        get {
            return _livesRemaining;
        }
        set {
            _livesRemaining = value;
            if (value < 0)
            {
                _livesRemaining = 0;
            }
            Save();
        }
    }
    public int maxLives { get; set; }
    public int maxHealth { get; set; }

    public int[] allLevels { get; set; }
    public DateTime nextRegenTime { get; set; }
    public GameObject levelSelection;

    public static GameStateController controller;

    public int[] unlockedLevels {
        get {
            return _unlockedLevels;
        }
        set {
            _unlockedLevels = value;
            levelSelection.SendMessage("UpdateLevelView");
        }
    }
    public int technology {
        get {
            return _technology;
        }
        set {
            _technology = value;
            Save();
        }
    }

    private int[] _unlockedLevels;
    private int _technology;
    private bool _completedTutorial;
    private int _livesRemaining;
    private const int RegenerateLifeLatency = 1; // in minutes
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

    void Start()
    { 
        Load();
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


    void InitialSetUp()
    {
        Debug.Log("In focus");
        // Set up defaults
        // All of these will be overwritten by the load file, if applicable
        allLevels = new int[Levels];
        unlockedLevels = new int[Levels];
        allLevels[0] = 0;
        unlockedLevels[0] = 1;
        for (int i = 1; i < allLevels.Length; i++)
        {
            allLevels[i] = 0;
            unlockedLevels[i] = 0;
        }
        nextRegenTime = DateTime.Now.AddMinutes(RegenerateLifeLatency);
        items = new Dictionary<string, int>();
        items.Add("maxHealthUpgrade", 50);
        _completedTutorial = false;
        maxHealth = 100;
        _technology = 100;
        maxLives = 3;

        _livesRemaining = 1;

        
    }

    // Called when the application first starts and each time it goes back into focus
    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            InitialSetUp();
            Load();
        }
        else
        {
            Debug.Log("Out of focus save event");
            Save();
        }
    }

    // Called when the user leaves the game (ex. switching apps) 
    void OnApplicationPause(bool pause)
    {
        Debug.Log("Pause. Bool status: " + pause);
        if (pause)
        {
            Debug.Log("Application pause save event");
            Save();
        }
    }

    // Called when the user completely exits the game
    void OnApplicationQuit()
    {
        Debug.Log("Application quit save event");
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
        state.items = items;

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
            items = state.items;

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
    public int livesRemaining { get; set; }
    public int maxLives { get; set; }
    public int maxHealth { get; set; }
    public int technology { get; set; }
    public double nextRegenTime { get; set; }
    public int[] allLevels;
    public int[] unlockedLevels;
    public Dictionary<string, int> items;

    public GameState()
    {
        allLevels = new int[3];
        unlockedLevels = new int[3];
    }
}
