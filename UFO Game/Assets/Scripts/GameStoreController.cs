using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoreController : MonoBehaviour
{
    public Dictionary<string, int> buyableObjectsMap;

    public Text itemCount;
    public Button purchaseButton;
    public Button backButton;
    public Text currency;

    //public GameState gameState;

    int count;
    int currencyAmount;


    // Start is called before the first frame update
    void Start()
    {
        // count = int.Parse(itemCount.text);
        // buyableObjectsMap = new Dictionary<string, int>();
        currencyAmount = GameStateController.controller.technology;
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

    public void purchase(){
        //GameState update to health and currency
        

        count = int.Parse(itemCount.text);

        // Debug.Log(currency.text.Substring(0,3).Length);
        // for(int i = 0; i < 4; i++){
        //     Debug.Log(currency.text[i]);
        // }
        // currencyAmount -= item["MaxHealthUpgrade"];
        currencyAmount -= count*10;
        currency.text = currencyAmount.ToString();
        

        GameStateController.controller.maxHealth += 10;
    }
}
