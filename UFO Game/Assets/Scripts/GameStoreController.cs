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

    //public GameState gameState;

    int count;
    int currencyAmount;


    // Start is called before the first frame update
    void Start()
    {
        // buyableObjectsMap = new Dictionary<string, int>();
        currencyAmount = GameStateController.controller.technology;
        Debug.Log("WOW :" + currencyAmount);
        currency.text = currencyAmount.ToString();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    //Adds 10 health to max health in game state

    //Adds store elements to store
    // void addToStore(){

    //     buyableObjectsMap.Add("MaxHealthUpgrade", 20);

    // }

    // Need to add Dictionary as argument
    public void purchase(){
        
        int totalCost;
        
        count = int.Parse(itemCount.text);

        totalCost = count*10;

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
        // currencyAmount -= item["MaxHealthUpgrade"];
        
    }
}
