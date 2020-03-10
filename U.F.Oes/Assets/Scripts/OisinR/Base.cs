using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public Text h, l;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Alien")
        {
            if (other.gameObject.GetComponent<Inventory>().hasItem)
            {
                if (other.gameObject.GetComponent<Inventory>().hasItem)
                {
                    other.gameObject.GetComponent<Inventory>().hasItem = false;
                }
                else
                {
                    other.gameObject.GetComponent<Inventory>().hasItem = false;
                }
            }

        }
    }
}
