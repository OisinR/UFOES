using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechItem : MonoBehaviour
{
    public bool highTech, lowTech;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if(other.gameObject.tag == "Alien")
        {
            Debug.Log(2);
            if (!other.gameObject.GetComponent<Inventory>().hasItem)
            {
                Debug.Log(3);
                if (highTech)
                {
                    other.gameObject.GetComponent<Inventory>().hasItem = true;
                    other.gameObject.GetComponent<Inventory>().highValue = true;
                }
                if (lowTech)
                {
                    other.gameObject.GetComponent<Inventory>().hasItem = true;
                    other.gameObject.GetComponent<Inventory>().highValue = false;
                }
                gameObject.SetActive(false);
            }

        }
    }
}
