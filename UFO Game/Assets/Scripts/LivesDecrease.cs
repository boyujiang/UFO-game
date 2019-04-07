using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesDecrease : MonoBehaviour
{
    public Text itemCount;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = int.Parse(itemCount.text[0].ToString());

    }

    // Update is called once per frame
    public void updateText()
    {   
        count = int.Parse(itemCount.text[0].ToString());

        if(count > 0){
            
            count--;
            itemCount.text = count.ToString();
            
            
        }
        
    }
}
