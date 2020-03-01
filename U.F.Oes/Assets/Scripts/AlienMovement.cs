using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AlienMovement : MonoBehaviour
{
    NavMeshPath path;
    [SerializeField] float distanceRemaining, distanceTravelled, distanceTotal, energyConsumed;
    public NavMeshAgent thisAlienAgent;
    GameObject ManagerObject;
    void Start()
    {
        thisAlienAgent = this.gameObject.GetComponent<NavMeshAgent>();
        ManagerObject = GameObject.FindGameObjectWithTag("Manager");
        path = new NavMeshPath();
    }

    void Update()
    {
        if (this.gameObject == ManagerObject.GetComponent<ControlScript>().selectedAlien)
        {
            MovementLogic();
        }
    }
    private void OnDrawGizmos()
    {
        
    }
    float PathDistance(NavMeshPath inputPath)
    {
        float distance = 0f;
        if (inputPath.status != NavMeshPathStatus.PathInvalid && path.corners.Length > 1)
        {
            for (int i = 1; i < path.corners.Length; i++)
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
       // var AS = this.GetComponent<AlienMovement>().thisAlienAgent;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(moveRay, out rH))
            {
                if (rH.collider.gameObject.tag == "Floor")
                {

                    if (thisAlienAgent.CalculatePath(rH.point, path))
                    {
                        thisAlienAgent.isStopped = false;
                        thisAlienAgent.SetDestination(rH.point);
                        distanceTotal = PathDistance(thisAlienAgent.path);
                        Debug.Log("Path length: " + distanceTotal);
                    }
                }

            }


        }
        distanceTravelled = DistanceTravelled(this.gameObject);

        if (Vector3.Distance(this.gameObject.transform.position, thisAlienAgent.destination) < 1f && thisAlienAgent.isStopped != true)
        {
            Debug.Log("Stopped");
            thisAlienAgent.isStopped = true;
        }


    }

    float DistanceTravelled(GameObject alien)
    {
        //Portion of script tasked with calculating the remaining distance, navmeshagent.remainingdistance often returns "infinity"
        // due to how it is calculated. This method adds up the distance
        // this portion of code was found at: https://github.com/dumbgamedev/general-playmaker/blob/master/path/calculateAgentDistanceByCorners.cs
        
        distanceRemaining = 0f;
        Vector3 previousCorner = thisAlienAgent.path.corners[0];

        int i = 1;
        while (i < thisAlienAgent.path.corners.Length)
        {
            Vector3 currentCorner = thisAlienAgent.path.corners[i];
            distanceRemaining += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }

        distanceTravelled = distanceTotal - distanceRemaining;

        return distanceTravelled;
    }
}
