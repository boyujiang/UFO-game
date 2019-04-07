using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesIncrease : MonoBehaviour
{
    public Text itemCount;
    
    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = int.Parse(itemCount.text[0].ToString());
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    
    //Populates the text field with quantity
    public void updateText(){
        
        count = int.Parse(itemCount.text[0].ToString());
        count++;
        itemCount.text = count.ToString();

    }
}
