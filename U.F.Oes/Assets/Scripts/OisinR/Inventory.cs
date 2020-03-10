using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool hasItem, canDisplay;
    public int itemNum;


    void Update()
    {
        if(CamControllerOis.target == gameObject)
        {
            canDisplay = true;
        }
        else
        {
            canDisplay = false;
        }
        if(hasItem && canDisplay)
        {
            ItemList.spriteNum = itemNum;
        }
        if(canDisplay && !hasItem)
        {
            ItemList.spriteNum = 0;
        }

    }
}
