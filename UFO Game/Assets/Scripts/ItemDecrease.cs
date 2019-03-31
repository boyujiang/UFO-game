using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDecrease : MonoBehaviour
{

    public Text itemCount;

    private string tempCount;

    public static int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void update(){
        
        count--;
    }
}
