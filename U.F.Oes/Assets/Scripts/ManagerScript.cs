using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    //// ManagerScript is Going to hold all information about the current gamestate. This will include: the global energy pool, 
    //the turn state etc. and will be used as an access point so that different scripts can communicate with each other (if 
    // necessarry) as much as possible. E.g. The alien movement will need access to the current turn state, the energy pool and other 
    // stuff so its needs the information from one source, the manager game object and its ManagerScript.cs component

    public float maxEnergy, replenRate, currentAlien, energyPool;
    public static float maxEnergy2;

    public static bool powerup;

    public bool playerTurn = true;

    private void Awake()
    {
        maxEnergy2 = maxEnergy;
        playerTurn = true;
    }

    private void Update()
    {
        //debugging for energy bar testing
        if(Input.GetKeyDown(KeyCode.R))
        {
            //maxEnergy = 100f;
            energyPool += 100f;
        }

        
        if(powerup)
        {
            //energyPool += replenRate;
            powerup = false;
        }

        
    }

    public void EndTurn()
    {
        playerTurn = false;
    }

}
