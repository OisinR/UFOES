using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Pathfinder : MonoBehaviour
{
    public int number;
    public Manager man;
    public float personalDistance;
    public Vector3 lastPoint;
    public LineRenderer lR;
    private PathMover pm;
    public List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };


    void Awake()
    {
        man = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        lR = GetComponent<LineRenderer>();
        pm = GetComponent<PathMover>();

        lR.positionCount = 0;
    }

    void Update()
    {
        if(number != Manager.numberSelected) { return; }

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
                    personalDistance += Vector3.Distance(points.Last(), hit.point);
                    Debug.Log(personalDistance);
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
}
