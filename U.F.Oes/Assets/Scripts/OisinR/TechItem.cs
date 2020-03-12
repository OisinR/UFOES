using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechItem : MonoBehaviour
{
    public int techItemNum;
    ManagerScript myManager;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Alien")
        {
            if (!other.gameObject.GetComponent<Inventory>().hasItem)
            {
                other.gameObject.GetComponent<Inventory>().itemNum = techItemNum;
                other.gameObject.GetComponent<Inventory>().hasItem = true;
                ManagerScript.ManagerAS.PlayOneShot(myManager.collect);
                gameObject.SetActive(false);
            }

        }
    }
}
