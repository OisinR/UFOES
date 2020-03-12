using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinCondition : MonoBehaviour
{
    public Base a, b, c;
    public GameObject winTxt;

    bool won;

    ManagerScript myManager;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerScript>();
    }

    void Update()
    {
        if(a.complete && b.complete && c.complete)
        {
            won = true;
            ManagerScript.ManagerAS.PlayOneShot(myManager.win);
            winTxt.SetActive(true);
        }
    }
}
