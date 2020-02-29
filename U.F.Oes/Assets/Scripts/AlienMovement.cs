using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AlienMovement : MonoBehaviour
{

    public NavMeshAgent thisAlienAgent;

    // Start is called before the first frame update
    void Start()
    {
        thisAlienAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
