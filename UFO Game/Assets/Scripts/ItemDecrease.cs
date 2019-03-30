using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDecrease : MonoBehaviour
{

    public Text itemCount;

    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void updateText(){
        
        itemCount.text = count--.ToString();
    }
}
