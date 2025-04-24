using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer; 
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // Detects if player clicks left mouse button
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
            {
                targetPosition = hit.point; // Set target position based on where player hits on map
                isMoving = true;
            }
        }

        // Moves player 
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Rotates the player towards movement direction
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0; // Keeps rotation flat on the ground
            if (direction.magnitude > 0.1f)
            {
                transform.forward = direction.normalized;
            }

            // Stops moving when reached the last hit target
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
}