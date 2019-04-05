using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoreController : MonoBehaviour
{
    public Dictionary<string, int> buyableObjectsMap;

    public Text itemCount;
    public Text currency;
    public Button rightScroll;
    public Button leftScroll;

    //public GameState gameState;

    int count;
    int currencyAmount;

    // Start is called before the first frame update
    void Awake()
    {
        currencyAmount = GameStateController.controller.technology;
        buyableObjectsMap = new Dictionary<string, int>();
        buyableObjectsMap.Add("MaxHealthUpgrade", 10);
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
        
        count = int.Parse(itemCount.text);

        totalCost = count*buyableObjectsMap["MaxHealthUpgrade"];

        if(currencyAmount > totalCost){
            currencyAmount -= totalCost;
            currency.text = currencyAmount.ToString();
            itemCount.text = "0";
        
            GameStateController.controller.technology = currencyAmount;

            GameStateController.controller.maxHealth += 10;
        }
        else {
            Debug.Log("Not enough currency");
        }
        
    }

}
