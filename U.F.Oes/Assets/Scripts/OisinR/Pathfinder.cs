using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Pathfinder : MonoBehaviour
{
    private float personalDistance;

    private LineRenderer lR;

    public List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };


    void Awake()
    {
        lR = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {         
            points.Clear();
            points.Add(transform.position);
        }

        if(Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (DistanceToLastPoint(hit.point) > 1f && (!(DistanceToLastPoint(hit.point) < 3f) && Manager.turnDistance < 20))
                {
                    Manager.turnDistance += Vector3.Distance(points.Last(), hit.point);
                    points.Add(hit.point);

                    lR.positionCount = points.Count;
                    lR.SetPositions(points.ToArray());

                    
                }
            }

        }

        

        else if (Input.GetButtonUp("Fire1"))
        {
            OnNewPathCreated(points);
        }

        Debug.Log(Manager.turnDistance);
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
