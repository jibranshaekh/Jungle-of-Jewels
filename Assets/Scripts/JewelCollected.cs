using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelCollected : MonoBehaviour
{
    public float rotationSpeed = 100f; 
    public static int collectedJewels = 0;
    public static int totalJewels = 5;
    public AudioClip collectSound; // In the inspector we will assign a jewel being picked up sound 

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime); // Rotates Jewels
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectedJewels++;
            if (JewelUI.instance != null)
            {
                JewelUI.instance.UpdateJewelCounter(collectedJewels, totalJewels); //Updates Jewel counter UI if player picks up jewel 
                }

            
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, 3.0f); //Plays collection sound effect
            }

            JewelManager jm = FindObjectOfType<JewelManager>(); //Removes the jewel from the map so that it does not cause music glitches
            if (jm != null)
            {
                jm.RemoveJewel(transform);
            }

            if (collectedJewels >= totalJewels) //Once all jewels are collected win screen is triggered 
            {
                Debug.Log("YOU WIN!");
                if (JewelUI.instance != null)
                {
                    JewelUI.instance.ShowWinScreen();
                    }
                    }
            Destroy(gameObject);
        }
    }
    public static void ResetJewelCount() //Restarts jewel counter when called by GameUI before restart
    {
    collectedJewels = 0;
    }

}