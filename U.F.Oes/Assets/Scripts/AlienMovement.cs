using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AlienMovement : MonoBehaviour
{

    public NavMeshAgent thisAlienAgent;
    
    void Start()
    {
        thisAlienAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        if(thisAlienAgent.destination!= null)
        {
           //Debug.Log(thisAlienAgent.remainingDistance);
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
