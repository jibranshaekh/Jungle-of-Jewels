using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public float fallThreshold = -10f;

    void Update()
    {
        if (transform.position.y < fallThreshold) // Will trigger game over is player falls below y position 
        {
            TriggerFallGameOver();
        }
    }

    void TriggerFallGameOver()
    {
        Debug.Log("Player fell off the map!"); //Triggers game over screen via GameUI 

        if (GameUI.instance != null)
        {
            GameUI.instance.ShowGameOverScreen("Out of Bounds!");
        }

        Time.timeScale = 0f; //Freezes the game 
    }
}