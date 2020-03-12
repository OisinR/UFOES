using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanLights : MonoBehaviour
{

    public LightItUp Lighting;

    ManagerScript myManager;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerScript>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Human")
        {
            Lighting.LightSwitch = true;
            ManagerScript.ManagerAS.PlayOneShot(myManager.lightswitch);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Human")
        {
            Lighting.LightSwitch = false;
            ManagerScript.ManagerAS.PlayOneShot(myManager.lightswitch);
        }
    }
}
