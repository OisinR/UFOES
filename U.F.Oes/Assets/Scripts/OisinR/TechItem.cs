using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechItem : MonoBehaviour
{
    public int techItemNum;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Alien")
        {
            if (!other.gameObject.GetComponent<Inventory>().hasItem)
            {
                other.gameObject.GetComponent<Inventory>().itemNum = techItemNum;
                other.gameObject.GetComponent<Inventory>().hasItem = true;
                gameObject.SetActive(false);
            }

        }
    }
}
