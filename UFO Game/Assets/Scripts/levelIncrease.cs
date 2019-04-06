using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelIncrease : MonoBehaviour
{
    public Text itemCount;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = int.Parse(itemCount.text);
    }

    // Update is called once per frame
    public void updateText()
    {
        count = int.Parse(itemCount.text);
        count++;
        itemCount.text = count.ToString();
    }
}
