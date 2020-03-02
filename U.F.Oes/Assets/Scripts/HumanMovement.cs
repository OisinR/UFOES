using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanMovement : MonoBehaviour
{
    [SerializeField] PathObject KitchenPath;
    [SerializeField] PathObject LivingPath;
    [SerializeField] PathObject BathroomPath;
    [SerializeField] PathObject BedroomPath;
    [SerializeField] PathObject GardenPath;

    [SerializeField] PathObject CurrentPath;
    [SerializeField] int CurrentPathIndex;
    [SerializeField] int CurrentPathPoint = 0;

    NavMeshAgent humanNavAgent;

    private void Awake()
    {
        humanNavAgent = this.gameObject.GetComponent<NavMeshAgent>();
        CurrentPath = new PathObject();
        CurrentPath = KitchenPath;
        CurrentPathPoint = 0;
    }

    private void Update()
    {
        switch (CurrentPathIndex)
        {
            case 1:
                CurrentPath = KitchenPath;
                break;
            case 2:
                CurrentPath = GardenPath;
                break;
            case 3:
                CurrentPath = BathroomPath;
                break;
            case 4:
                CurrentPath = BedroomPath;
                break;
            case 5:
                CurrentPath = LivingPath;
                break;
        }

        MoveToWaypoint(CurrentPath);
    }

    void MoveToWaypoint(PathObject following)
    {
        Transform destPoint = following.pathPoints[CurrentPathPoint];

        humanNavAgent.SetDestination(following.pathPoints[CurrentPathPoint].position);

        if(Vector3.Distance(this.transform.position, destPoint.position)< 5)
        {
            if(CurrentPathPoint<following.pathPoints.Count-1)
            {
                CurrentPathPoint++;
            }
            else
            {
                Debug.Log("Switching");
                CurrentPathIndex = Random.Range(1, 6);
                CurrentPathPoint = 0;

            }

        }
    }


}
