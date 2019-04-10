using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoreController : MonoBehaviour
{
    public Dictionary<string, int> buyableObjectsMap;

    public Text healthCount;
    public Text currency;
    public Text currencyNotif;
    public Text levelCount;
    public Text livesCount;

    int countHealth;
    int countLevel;
    int currencyAmount;
    int countLives;


    // Start is called before the first frame update
    void Awake()
    {
        currencyAmount = GameStateController.controller.technology;
        Debug.Log("Game Store:" + GameStateController.controller.technology);
        buyableObjectsMap = new Dictionary<string, int>();
        buyableObjectsMap.Add("MaxHealthUpgrade", 10);
        buyableObjectsMap.Add("AddNewLevel", 50);
        currency.text = "Technology: " + currencyAmount.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        currencyAmount = GameStateController.controller.technology;
        currency.text = "Technology: " + currencyAmount.ToString();
        Debug.Log("Game Store:" + GameStateController.controller.technology);
    }




    public void purchase(){
        
        int totalCost;

        countHealth = int.Parse(healthCount.text); //Parses text boxes for initial value 0
        countLevel = int.Parse(levelCount.text);
        countLives = int.Parse(livesCount.text[0].ToString());

        totalCost = countHealth*buyableObjectsMap["MaxHealthUpgrade"];
        totalCost += countLevel*buyableObjectsMap["AddNewLevel"];

        if(currencyAmount > totalCost){
            GameStateController.controller.technology -= totalCost;
            currency.text = "Technology: " + GameStateController.controller.technology.ToString();
            healthCount.text = "0";
        
            //GameStateController.controller.technology = currencyAmount; //Changes GameState i.e. player stats

            GameStateController.controller.maxHealth += 10*countHealth;
        }
        else {
            currencyNotif.text = "Not Enough Technology!";
        }
        
        if(countLives != 0){
            Application.OpenURL("http://play.google.com");
        }
        
    }

}
