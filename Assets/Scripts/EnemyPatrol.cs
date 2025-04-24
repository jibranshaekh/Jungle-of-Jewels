using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    private int currentWaypointIndex = 0;
    // Abovd sets the waypoints for enemies that can be assigned in the inspector and speed travelled between them 

    public Transform player;
    public float detectionRange = 5f;
    public float fieldOfViewAngle = 60f;
    // Sets the parameteres for detecting player 

    public float loseSightDelay = 3f;
    private float timeSinceLastSeen = 0f;
    private bool isChasing = false;
    // Sets the chase parameteres 

    private Rigidbody rb;

    private EnemyDetectionBar detectionBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        detectionBar = GetComponent<EnemyDetectionBar>(); // Referneces EnemyDectionbar script
    }

    void Update()
    {
        // Applies gravity so enemies can stay grounded
        if (rb != null)
        {
            rb.velocity += Vector3.down * 9.81f * Time.deltaTime;
        }

        if (isChasing)
        {
            ChasePlayer();

            if (!CanSeePlayer())  // If enemy loses sight of player
            {
                timeSinceLastSeen += Time.deltaTime;

                if (timeSinceLastSeen >= loseSightDelay)
                {
                    isChasing = false;
                    timeSinceLastSeen = 0f;
                }

                if (MusicManager.instance != null)
                {
                    MusicManager.instance.SetChased(false);
                    } // Changes the music back to the default background
            }
            else
            {
                timeSinceLastSeen = 0f;

                // Updates the detection bar if player is visible
                if (detectionBar != null)
                    detectionBar.SetPlayerVisible(true);
            }
        }
        else
        {
            Patrol();
            CheckForPlayer(); // Returns to normal patrol
        }

        //  Game Over screen is called if detection bar is fully filled
        if (detectionBar != null && detectionBar.IsFullyDetected())
        {
            GameOver();
        }
    }

    void Patrol() //Moves enemy between assigned waypoints 
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero) // Rotates player towards the next waypoint, otherwise it the spotlight does not change direction
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void CheckForPlayer()
    {
        if (CanSeePlayer())
        {
            isChasing = true;

            if (MusicManager.instance != null) //Music chnages to chase music once the player is visible 
            {
                MusicManager.instance.SetChased(true);
                }

            //Update detection bar on initial sighting
            if (detectionBar != null)
                detectionBar.SetPlayerVisible(true);
        }
    }

    bool CanSeePlayer()
    {
        if (player == null) return false;

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= detectionRange && angleToPlayer <= fieldOfViewAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }  // Above function returns true if player can be seen in line of sight which will trigger the chase 

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, player.position) < 1.5f)
        {
            GameOver();
        } 

        // While chasing this updates the bar
        if (CanSeePlayer() && detectionBar != null)
        {
            detectionBar.SetPlayerVisible(true);
        }
    }

    void GameOver() //Displays game over script via GameUI 
    {
        Debug.Log("Game Over! Caught by an enemy.");
        if (GameUI.instance != null)
        {
            GameUI.instance.ShowGameOverScreen("You Were Caught!");
        }
    }
}