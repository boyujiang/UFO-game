using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStoreController : MonoBehaviour
{
    //public Map<String, Integer> buyableObjectsMap;

    public Text itemCount;
    public GameObject purchaseButton;

    public GameState gs;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = int.ParseInt(itemCount.text);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void upgradePlayer(){

        int healthIncrease = 10; //Can change value


    }
}
