using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelManager : MonoBehaviour
{
    public Transform player;
    public Transform[] jewels;
    public float detectionRadius = 10f; // This will trigger the jewel music if player is within proximity of jewel

    private void Update()
    {
        if (player == null || jewels.Length == 0 || MusicManager.instance == null) //if these parameters do not exist then this if statement avoids any errors 
            return;

        Transform closestJewel = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform jewel in jewels) //Detects closest jewel to player
        {
            float distance = Vector3.Distance(jewel.position, player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestJewel = jewel;
            }
        }

        // Checks if we are near nearest jewel
        bool isNearJewel = (closestDistance <= detectionRadius);
        MusicManager.instance.SetProximityActive(isNearJewel);
    }

    public void RemoveJewel(Transform jewel) //Called by JewelCollected and removes collected jewel's from the list so the music does not trigger 
    {
    List<Transform> updatedList = new List<Transform>(jewels);
    if (updatedList.Contains(jewel))
    {
        updatedList.Remove(jewel);
        jewels = updatedList.ToArray();
    }
    }
}
