using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechItem : MonoBehaviour
{
    public int techItemNum;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if(other.gameObject.tag == "Alien")
        {
            Debug.Log(2);
            if (!other.gameObject.GetComponent<Inventory>().hasItem)
            {
                other.gameObject.GetComponent<Inventory>().itemNum = techItemNum;
                gameObject.SetActive(false);
            }

        }
    }
}
