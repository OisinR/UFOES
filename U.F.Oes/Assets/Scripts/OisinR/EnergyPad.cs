using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{

    bool activated;


    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.gameObject.tag == "Alien")
        {
            ManagerScript.powerup = true;
            activated = true;
            gameObject.SetActive(false);
        }
    }
}
