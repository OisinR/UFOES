using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{



    void Start()
    {
        
    }




    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        //Manager.turnDistanceMax += 10;
        Manager.turnDistanceTotal += 10;
    }
}
