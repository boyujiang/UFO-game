﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIncrease : MonoBehaviour
{
    public Text itemCount;
    public Button purchaseButton;
    
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = int.Parse(itemCount.text);
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    
    //Populates the text field with quantity
    public void updateText(){
        
        count = int.Parse(itemCount.text);
        count++;
        itemCount.text = count.ToString();

    }
}
