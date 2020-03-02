using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathMover : MonoBehaviour
{
    public bool turnOver;

    
    private Pathfinder pf;
    private NavMeshAgent agent;
    private Queue<Vector3> pathPoints = new Queue<Vector3>();


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        pf = GetComponent<Pathfinder>();

        pf.OnNewPathCreated += SetPoints;
    }

    private void SetPoints(IEnumerable<Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
    }


    void Update()
    {
        if (turnOver && Vector3.Distance(transform.position, pf.lastPoint) > 1f)
        {
            UpdatePathing();
        }
        if (turnOver && Vector3.Distance(transform.position, pf.lastPoint) < 1f)
        {
            turnOver = false;
            pf.lR.positionCount = 0;
        }
    }

    void UpdatePathing()
    {
        if(ShouldSetDestination())
        {
            agent.SetDestination(pathPoints.Dequeue());
        }


    }

    private bool ShouldSetDestination()
    {
        if(pathPoints.Count == 0)
        {
            return false;
        }
        if(agent.hasPath == false || agent.remainingDistance < 0.5f)
        {
            return true;
        }
        return false;
    }
}
