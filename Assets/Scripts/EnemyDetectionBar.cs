using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectionBar : MonoBehaviour
{
    public Slider detectionSlider;
    public GameObject detectionUI;

    public float detectionSpeed = 30f;
    public float detectionDecay = 20f;

    private float detectionLevel = 0f;
    private bool playerVisible = false;

    public void SetPlayerVisible(bool visible) // Called by enemyPatrol script, this will trigger the detction bar to appear and begin filling
    {
        playerVisible = visible;

        if (visible && !detectionUI.activeSelf)
            detectionUI.SetActive(true);
    }

    void Update()
    {
        if (playerVisible)
        {
            detectionLevel += detectionSpeed * Time.deltaTime; // Increases detection level if more visible 
        }
        else
        {
            detectionLevel -= detectionDecay * Time.deltaTime; //Decreases if less visibile 
        }

        detectionLevel = Mathf.Clamp(detectionLevel, 0f, 100f);
        detectionSlider.value = detectionLevel;

        if (detectionLevel <= 0f && detectionUI.activeSelf) // Hides the bar if the detection reaches 0
        {
            detectionUI.SetActive(false);
        }

        playerVisible = false; // Resets player visibility
    }

    public bool IsFullyDetected() 
    {
        return detectionLevel >= 100f;
    }
}
