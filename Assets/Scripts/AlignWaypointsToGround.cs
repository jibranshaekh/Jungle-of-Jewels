using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWaypointsToGround : MonoBehaviour
{
    public LayerMask groundLayer; // Assign layer in inspector to "Ground" to ensure the enemy patrol paths are in line with the ground 

    void Start()
    {
        AlignWaypoints(); 
    }

    void AlignWaypoints() //
    {
        foreach (Transform waypoint in transform)
        {
            RaycastHit hit;
            if (Physics.Raycast(waypoint.position + Vector3.up * 10f, Vector3.down, out hit, Mathf.Infinity, groundLayer)) //creates raycast from the top of the waypoint until it hits ground layer 
            {
                waypoint.position = new Vector3(waypoint.position.x, hit.point.y, waypoint.position.z); // Moves the waypoint's Y position to the hit point of the raycast 
            }
        }
    }
}
