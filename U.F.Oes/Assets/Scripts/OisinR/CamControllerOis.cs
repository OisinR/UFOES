using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerOis : MonoBehaviour
{
    //Reminder of stuff to do. put in text that appears and fades whenever you go into a room. have it be center bottom screen.
    //TIP: Switch to other aliens when low on energy; appears under aien selection


    public static GameObject target;
    public GameObject[] targets;
    public  float minY, maxY, minX, maxX, minZ, maxZ, moveSpeed;
    bool changedAlien;
    int currentAlien;
    float counter;
    /*
     *  -x to go up + x to go down          : z
     *  -z to go left + z to go right       : x
     *  +y go upVert + -y to go downVert    : y
     */

    private void Start()
    {
        currentAlien = 0;
    }

    void Update()
    {

        /*
        //up and down
        minX = target.transform.position.x - 20;
        maxX = target.transform.position.x + 20;

        //left and right
        minZ = target.transform.position.z - 30;
        maxZ = target.transform.position.z + 30;

        //scroll
        minY = target.transform.position.y + 10;
        maxY = target.transform.position.y + 80;
        */
        
        if(changedAlien)
        {
            ChangeAlien(currentAlien);

        }

        Vector3 newPos = Vector3.Lerp(transform.position, transform.position + (Input.GetAxis("Horizontal") * Vector3.forward) + (Input.GetAxis("Vertical") * Vector3.left) +
                                        (-Input.mouseScrollDelta.y * Vector3.up * 4f), moveSpeed);


        if (newPos.y < minY)
        {
            newPos.y = minY;
        }
        if (newPos.y > maxY)
        {
            newPos.y = maxY;
        }
        if (newPos.x < minX)
        {
            newPos.x = minX;
        }
        if (newPos.x > maxX)
        {
            newPos.x = maxX;
        }
        if (newPos.z < minZ)
        {
            newPos.z = minZ;
        }
        if (newPos.z > maxZ)
        {
            newPos.z = maxZ;
        }
        transform.position = newPos;
    }



    void ChangeAlien(int i)
    {
        changedAlien = true;
        transform.position = Vector3.Slerp(transform.position,new Vector3 (targets[i].transform.position.x,transform.position.y-0.01f, targets[i].transform.position.z), 0.1f);
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            changedAlien = false;
        }

    }

    public void Button(int i)
    {
        counter = 0.5f;
        currentAlien = i;
        ChangeAlien(i);
    }
}
