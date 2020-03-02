using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlScript : MonoBehaviour
{
    ManagerScript myManager;
    [SerializeField] GameObject Alien1, Alien2, Alien3;
    public GameObject selectedAlien;
    [SerializeField]List<GameObject> Aliens;
    [SerializeField] Image energyPool, potentialEnergy;
    NavMeshPath path;
    LayerMask onlyRaycastMask;
    //[SerializeField] float distanceRemaining, distanceTravelled, distanceTotal, energyConsumed;

    void Start()
    {
        
        Aliens = new List<GameObject>();
        foreach(AlienMovement alien in  GameObject.FindObjectsOfType<AlienMovement>())
        {
            Aliens.Add(alien.gameObject);
        }
        selectedAlien = Alien1;
        path = new NavMeshPath();
        myManager = this.gameObject.GetComponent<ManagerScript>();
        
    }
    
    

    void FixedUpdate()
    {
        RaycastHit rH;
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            selectedAlien = Aliens[0];

        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            selectedAlien = Aliens[1];

        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            selectedAlien = Aliens[2];

        }

        var AS = selectedAlien.GetComponent<AlienMovement>().thisAlienAgent;

        //Selecting Alien: At this point their is an issue with raycasts that I cant figure out, Im adding key input selection as a debugging tool
        //if (Physics.Raycast(this.transform.position, moveRay.direction*100f, out rH, Mathf.Infinity, 1 << LayerMask.NameToLayer("OnlyRaycasts")))
        if (Physics.Raycast(moveRay, out rH, 1 << LayerMask.NameToLayer("OnlyRaycasts")))
        {
                if (rH.collider.gameObject.tag == "Alien")
                {
                    //Debug.Log("Overplayer");
                    if(Input.GetMouseButtonDown(0))
                    {
                        selectedAlien = rH.collider.gameObject.transform.parent.gameObject;
                        Debug.Log(selectedAlien.name);
                    }
                    
                }
            
        }
        EnergyBarUpdates();

    }
    void EnergyBarUpdates()
    {
        energyPool.transform.localScale = new Vector3(energyPool.transform.localScale.x, myManager.energyPool / myManager.maxEnergy, energyPool.transform.localScale.z);
        potentialEnergy.transform.localScale = new Vector3(potentialEnergy.transform.localScale.x, Mathf.Min(selectedAlien.GetComponent<AlienMovement>().potentialDistance/ myManager.energyPool,1f), potentialEnergy.transform.localScale.z);

    }

    private void OnDrawGizmos()
    {
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position);
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(this.gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Gizmos.DrawRay(moveRay);
    }
}
