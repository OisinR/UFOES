using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasItem, canDisplay;
    public int itemNum;


    void Update()
    {
        if(hasItem && canDisplay)
        {
            ItemList.spriteNum = itemNum;
        }
        else
        {

        }
    }
}
