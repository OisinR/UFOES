using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{

    bool activated;

    void Start()
    {
        
    }




    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            Manager.turnDistanceTotal += 10;
            activated = true;
        }
    }
}
