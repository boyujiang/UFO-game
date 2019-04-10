using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeMenuScene : MonoBehaviour
{
    public void changemenuScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void GoBackToLevelSelect()
    {
        GameStateController.controller.completedTutorial = true;
        SceneManager.LoadScene(0);
    }
}
