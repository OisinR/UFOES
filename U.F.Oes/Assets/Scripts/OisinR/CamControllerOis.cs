using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControllerOis : MonoBehaviour
{
    public static GameObject target;

    public  float minY, maxY, minX, maxX, minZ, maxZ, moveSpeed;


    /*
     *  -x to go up + x to go down          : z
     *  -z to go left + z to go right       : x
     *  +y go upVert + -y to go downVert    : y
     */
    
    void Update()
    {
        //up and down
        minX = target.transform.position.x - 20;
        maxX = target.transform.position.x + 20;

        //left and right
        minZ = target.transform.position.z - 30;
        maxZ = target.transform.position.z + 30;

        //scroll
        minY = target.transform.position.y + 10;
        maxY = target.transform.position.y + 80;

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
}
