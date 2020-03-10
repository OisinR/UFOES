using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] float minY, maxY, minX, maxX, minZ, maxZ, moveSpeed;
    void FixedUpdate()
    {
        Vector3 newPos =Vector3.Lerp(transform.position, transform.position + (Input.GetAxis("Horizontal") * Vector3.forward) +
                                                (Input.GetAxis("Vertical") * Vector3.left) +
                                                (-Input.mouseScrollDelta.y * Vector3.up * 4f), moveSpeed);
        
        if(newPos.y<minY)
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
