using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public int item1, item2;
    bool collected1, collected2;

    public bool complete;
    public GameObject[] itemsDisplay;
    Inventory script;

    ManagerScript myManager;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerScript>();
    }


    private void Update()
    {
        if(collected1)
        {
            itemsDisplay[0].SetActive(false);
        }

        if (collected2)
        {
            itemsDisplay[1].SetActive(false);
        }
        
        if(collected1 && collected2)
        {
            ManagerScript.ManagerAS.PlayOneShot(myManager.basecomplete);
            complete = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Alien")
        {
            script = other.gameObject.GetComponent<Inventory>();

            if (script.hasItem)
            {
                if (script.itemNum == item1)
                {
                    script.itemNum = 0;
                    collected1 = true;
                    script.hasItem = false;
                }
                if (script.itemNum == item2)
                {
                    script.itemNum = 0;
                    collected2 = true;
                    script.hasItem = false;
                }
            }

        }
    }
}
