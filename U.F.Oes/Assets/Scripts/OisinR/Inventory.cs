using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasItem;
    public bool highValue;


    public GameObject high, low;


    void Update()
    {
        if(hasItem)
        {
            if(highValue)
            {
                high.SetActive(true);
            }
            else
            {
                low.SetActive(true);
            }
        }
        else
        {
            high.SetActive(false);
            low.SetActive(false);
        }
    }
}
