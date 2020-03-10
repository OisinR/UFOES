using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Sprite[] items;
    public Image display;

    public static int spriteNum;


    void Update()
    {
        display.overrideSprite = items[spriteNum];
    }
}
