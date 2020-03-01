using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AlienMovement : MonoBehaviour
{
    Transform targetPos;
    NavMeshPath path, agentCurrentPath;
    [SerializeField] float distanceRemaining, distanceTravelled, distanceTotal, energyConsumed;
    [SerializeField] GameObject myNavLine, myDistance;
    public NavMeshAgent thisAlienAgent;
    GameObject ManagerObject;
    void Start()
    {
        if(this.gameObject.GetComponent<LineRenderer>()==null)
        {
            this.gameObject.gameObject.AddComponent<LineRenderer>();

        }

        targetPos = new GameObject().transform;//debugging target
        thisAlienAgent = this.gameObject.GetComponent<NavMeshAgent>();
        ManagerObject = GameObject.FindGameObjectWithTag("Manager");
        path = new NavMeshPath();
        agentCurrentPath = new NavMeshPath();
    }

    void LateUpdate()
    {
        
        if (this.gameObject == ManagerObject.GetComponent<ControlScript>().selectedAlien)
        {
            MovementLogic();
            
        }

        DistanceRender();
        NewPathRender();
    }
   
    

    void MovementLogic()
    {
        RaycastHit rH;
        Ray moveRay =  Camera.main.ScreenPointToRay(Input.mousePosition);
        
        
        if (Physics.Raycast(moveRay, out rH))
        {
            if (rH.collider.gameObject.tag == "Floor")
            {

                if (thisAlienAgent.CalculatePath(rH.point, path))
                {
                    thisAlienAgent.isStopped = false;

                    targetPos.position = rH.point;//debugging

                    if (Input.GetMouseButtonDown(0))
                    {
                        agentCurrentPath = path;
                        thisAlienAgent.SetPath(agentCurrentPath);
                        distanceTotal = PathDistance(thisAlienAgent.path);
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

    #region Nav Mesh Path rendering
    void DistanceRender()
    {
        
        var LR = myDistance.GetComponent<LineRenderer>();
        
        LR.positionCount = thisAlienAgent.path.corners.Length;
        
        LR.SetPositions(thisAlienAgent.path.corners);
        if (this.gameObject!= ManagerObject.GetComponent<ControlScript>().selectedAlien)
        {
           // LR.enabled = false;
        }
        else
        {
            LR.enabled = true;
        }
        

    }

    void NewPathRender()
    {
        var LR = myNavLine.GetComponent<LineRenderer>();
        
        
        LR.positionCount = path.corners.Length;

        LR.SetPositions(path.corners);
        if (this.gameObject != ManagerObject.GetComponent<ControlScript>().selectedAlien)
        {
            LR.enabled = false;
        }
        else
        {
            LR.enabled = true;
        }

    }
    #endregion



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


    private void OnDrawGizmos()
    {
        

    }

}
