using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    [SerializeField] GameObject Alien1, Alien2, Alien3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit rH;
        Ray moveRay = Camera.main.ScreenPointToRay(Input.mousePosition);
       if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(moveRay, out rH))
            {
                Alien1.GetComponent<AlienMovement>().thisAlienAgent.SetDestination(rH.point);
            }


        }
    }
}
