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
}
