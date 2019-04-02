﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectionController : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    public Button[] levelButtons;

    // Start is called before the first frame update
    void Start()
    {
        levelButtons = new Button[3];
        levelButtons[0] = GameObject.Find("Level_1_Btn").GetComponent<Button>();
        levelButtons[1] = GameObject.Find("Level_2_Btn").GetComponent<Button>();
        levelButtons[2] = GameObject.Find("Level_3_Btn").GetComponent<Button>();


        int[] levels = GameStateController.controller.unlockedLevels;
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i] == 0) // Not yet unlocked
            {
                Debug.Log("locked :" + i);
                levelButtons[i].interactable = false;
            }
        }
    }


    public void OpenStore()
    {
        // The index of the Level Selection is 0. Game Store is set as 1 in build 
        //GameStateController.controller.unlockedLevels[0] = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateLevelView()
    {
        Debug.Log("It WORKS");
    }


    public void Quit()
    {
        Application.Quit();
    }
}
