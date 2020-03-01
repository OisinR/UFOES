using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlScript : MonoBehaviour
{
    ManagerScript myManager;
    [SerializeField] GameObject Alien1, Alien2, Alien3;
    [SerializeField] GameObject selectedAlien;
    NavMeshPath path;
    [SerializeField] float distanceRemaining, distanceTravelled, distanceTotal, energyConsumed;

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
        MovementLogic();
       if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(moveRay, out rH))
            {
                if (rH.collider.gameObject.tag == "Alien" && Vector3.Distance(selectedAlien.transform.position, AS.destination) < 1f)
                {
                    selectedAlien = rH.collider.gameObject;
                    Debug.Log(selectedAlien.name);
                }
            }
        }
    }
    float PathDistance(NavMeshPath inputPath)
    {
        float distance = 0f;
        if (inputPath.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
        {
            for(int i =1;i<path.corners.Length; i++)
            {
                distance += Vector3.Distance(path.corners[i], path.corners[i - 1]);
            }
        }
        return distance;
    }
    

    void MovementLogic()
    {
        RaycastHit rH;
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var AS = selectedAlien.GetComponent<AlienMovement>().thisAlienAgent;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(moveRay, out rH))
            {
                if (rH.collider.gameObject.tag == "Floor")
                {
                    
                    if (AS.CalculatePath(rH.point, path))
                    {
                        AS.isStopped = false;
                        AS.SetDestination(rH.point);
                        distanceTotal = PathDistance(AS.path);
                        Debug.Log("Path length: " + distanceTotal);
                    }
                }

            }


        }
        distanceTravelled = DistanceTravelled(selectedAlien);
        
        if (Vector3.Distance(selectedAlien.transform.position, AS.destination) < 1f && AS.isStopped != true)
        {
            Debug.Log("Stopped");
            AS.isStopped = true;
        }

        
    }

    float DistanceTravelled(GameObject alien)
    {
        //Portion of script tasked with calculating the remaining distance, navmeshagent.remainingdistance often returns "infinity"
        // due to how it is calculated. This method adds up the distance
        // this portion of code was found at: https://github.com/dumbgamedev/general-playmaker/blob/master/path/calculateAgentDistanceByCorners.cs

        var AS = alien.GetComponent<AlienMovement>().thisAlienAgent;
        distanceRemaining = 0f;
        Vector3 previousCorner = AS.path.corners[0];

        int i = 1;
        while (i < AS.path.corners.Length)
        {
            Vector3 currentCorner = AS.path.corners[i];
            distanceRemaining += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }

        distanceTravelled = distanceTotal - distanceRemaining;

        return distanceTravelled;
    }
}
