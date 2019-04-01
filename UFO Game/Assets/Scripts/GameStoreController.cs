using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStoreController : MonoBehaviour
{
    public Dictionary<string, int> buyableObjectsMap;

    public Text itemCount;
    public GameObject purchaseButton;

    //public GameState gameState;

    int count;

    // Start is called before the first frame update
    void Start()
    {
        count = int.Parse(itemCount.text);
        buyableObjectsMap = new Dictionary<string, int>();
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    //Adds 10 health to max health in game state
    public void upgradePlayer(){

        int healthIncrease = 10; //Can change value


    }

    //Loads the Game-Store scene
    public void generateUI(){

        SceneManager.LoadScene("game-store");

    }

    //Adds store elements to store
    void addToStore(){

        buyableObjectsMap.Add("MaxHealthUpgrade", 100);

    }

    public void purchase(Dictionary<string, int> item){
        //GameState update to health and currency
    }
}
