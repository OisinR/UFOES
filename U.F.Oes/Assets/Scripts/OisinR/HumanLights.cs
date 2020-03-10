using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLights : MonoBehaviour
{

    public LightItUp Lighting;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Human")
        {
            Lighting.LightSwitch = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Human")
        {
            Lighting.LightSwitch = false;
        }
    }
}
