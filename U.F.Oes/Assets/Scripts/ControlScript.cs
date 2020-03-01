using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlScript : MonoBehaviour
{
    ManagerScript myManager;
    [SerializeField] GameObject Alien1, Alien2, Alien3;
    public GameObject selectedAlien;
    NavMeshPath path;
    //[SerializeField] float distanceRemaining, distanceTravelled, distanceTotal, energyConsumed;

    void Start()
    {
        selectedAlien = Alien1;
        path = new NavMeshPath();
        myManager = this.gameObject.GetComponent<ManagerScript>();
    }
    
    void FixedUpdate()
    {
        RaycastHit rH;
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var AS = selectedAlien.GetComponent<AlienMovement>().thisAlienAgent;
        //MovementLogic();
       if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(moveRay, out rH))
            {
                if (rH.collider.gameObject.tag == "Alien")
                {
                    selectedAlien = rH.collider.gameObject;
                    Debug.Log(selectedAlien.name);
                }
            }
        }
    }
}
