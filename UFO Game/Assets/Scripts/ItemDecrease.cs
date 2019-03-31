using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDecrease : MonoBehaviour
{

    public Text itemCount;

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

    //Populates the text field with quantity. Value changes only if quantity is greater than 0
    public void updateText(){

        count = int.Parse(itemCount.text);

        if(count > 0){
            
            count--;
            itemCount.text = count.ToString();
            Debug.Log(count);
            
        }
    }
}
