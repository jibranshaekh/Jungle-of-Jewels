using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //  Assign in inspector to be the player 
    public Vector3 offset = new Vector3(0f, 5f, -7f); // Default camera position
    public float rotationSpeed = 3f; // This will be mouse sensitivity
    public float zoomSpeed = 5f; // Zoom sped 
    public float minZoom = 3f; // Closest we can zoom in
    public float maxZoom = 15f; // Furthest we can zoom out
    public float minYAngle = 10f; // Prevents player from looking under the map
    public float maxYAngle = 80f; // Prevents player from being able to look too high

    private float currentZoom = 10f;
    private float rotationY = 0f;
    private float rotationX = 20f;
    private bool isFreeLooking = false;

    void Start()
    {
        currentZoom = offset.magnitude;
        Cursor.lockState = CursorLockMode.None; // Keeps the cursor free
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Allows controlling the camera with right mouse button being held
        if (Input.GetMouseButtonDown(1)) 
        {
            isFreeLooking = true;
            Cursor.lockState = CursorLockMode.Locked; 
        }
        if (Input.GetMouseButtonUp(1)) 
        {
            isFreeLooking = false;
            Cursor.lockState = CursorLockMode.None; 
        }

        // Rotates the camera if right mouse button is held 
        if (isFreeLooking)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            rotationY += mouseX;
            rotationX -= mouseY; 
            rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle); // This prevents being able to look under the map
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 zoomedOffset = new Vector3(0f, 0f, -currentZoom);

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
            Vector3 rotatedOffset = rotation * zoomedOffset;

            transform.position = target.position + rotatedOffset;
            transform.LookAt(target.position); //keeps camera positioned to follow the player
        }
    }
}