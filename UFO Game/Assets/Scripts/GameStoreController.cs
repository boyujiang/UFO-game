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
    public Button rightScroll;
    public Button leftScroll;
    public Text currencyNotif;
    public Text levelCount;

    //public GameState gameState;

    int countHealth;
    int countLevel;
    int currencyAmount;

    // Start is called before the first frame update
    void Awake()
    {
        currencyAmount = GameStateController.controller.technology;
        buyableObjectsMap = new Dictionary<string, int>();
        buyableObjectsMap.Add("MaxHealthUpgrade", 10);
        buyableObjectsMap.Add("AddNewLevel", 50);
        currency.text = currencyAmount.ToString();
        
        if(buyableObjectsMap.Count == 1){
            rightScroll.interactable = false;
            leftScroll.interactable = false;
        }

    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    //Adds 10 health to max health in game state

    //Adds store elements to store 
    // void addToStore(){

    //     buyableObjectsMap.Add("MaxHealthUpgrade", 10);

    // }

    // Need to add Dictionary as argument
    public void purchase(){
        
        int totalCost;

        countHealth = int.Parse(healthCount.text);
        countLevel = int.Parse(levelCount.text);

        totalCost = countHealth*buyableObjectsMap["MaxHealthUpgrade"];
        totalCost += countLevel*buyableObjectsMap["AddNewLevel"];

        if(currencyAmount > totalCost){
            currencyAmount -= totalCost;
            currency.text = currencyAmount.ToString();
            healthCount.text = "0";
        
            GameStateController.controller.technology = currencyAmount;

            GameStateController.controller.maxHealth += 10*countHealth;
        }
        else {
            currencyNotif.text = "Not Enough Technology!";
        }
        
        
    }

}
