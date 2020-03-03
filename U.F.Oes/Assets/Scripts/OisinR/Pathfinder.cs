using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinder : MonoBehaviour
{

    public NavMeshAgent agent;
    public int number;
    public Manager man;
    public float personalDistance;
    public Vector3 lastPoint;
    public LineRenderer lR;
    private PathMover pm;
    public List<Vector3> points = new List<Vector3>();
    private NavMeshPath path;
    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };


    void Awake()
    {
        
        man = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        lR = GetComponent<LineRenderer>();
        pm = GetComponent<PathMover>();
        agent = GetComponent<NavMeshAgent>();
        lR.positionCount = 0;
    }

    void Update()
    {
        path = new NavMeshPath();
        if (number != Manager.numberSelected) { return; }

        if(Input.GetButtonDown("Fire2") && !pm.turnOver)
        {         
            points.Clear();
            points.Add(transform.position);
            man.turnDistance -= personalDistance;
            personalDistance = 0;
        }

        if(Input.GetButton("Fire2"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (DistanceToLastPoint(hit.point) > 1f && (!(DistanceToLastPoint(hit.point) > 3f) && man.turnDistance < Manager.turnDistanceMax))
                {
                    //Make sure the points are within walking reach of each other/not through a wall
                    if (!Navmeshable(hit.point)) { return; }

                    personalDistance += Vector3.Distance(points.Last(), hit.point);
                    //Debug.Log(personalDistance);
                    man.turnDistance += Vector3.Distance(points.Last(), hit.point);
                    points.Add(hit.point);

                    lR.positionCount = points.Count;
                    lR.SetPositions(points.ToArray());

                    lastPoint = hit.point;
                    
                }
            }

        }

        

        else if (Input.GetButtonUp("Fire2") && !pm.turnOver)
        {
            OnNewPathCreated(points);
        }

        //Debug.Log(Manager.turnDistance);
    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if(!points.Any())
        {
            return Mathf.Infinity;
        }
        return Vector3.Distance(points.Last(), point);
    }


    private bool Navmeshable(Vector3 point)
    {
        NavMesh.CalculatePath(points.Last(), point, NavMesh.AllAreas, path);
        float total = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            total += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }
        if (total > 2f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
