using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinCondition : MonoBehaviour
{
    public Base a, b, c;
    public GameObject winTxt;

    bool won;

    void Update()
    {
        if(a.complete && b.complete && c.complete)
        {
            won = true;
            winTxt.SetActive(true);
        }
    }
}
