using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileColliderAddIn : MonoBehaviour
{
    void Start()
    {
        // Get all child objects within the ground tile
        foreach (Transform tile in transform)
        {
            // Checks if object has a collider if not it will add one by default used for building environment with tile pallete
            if (!tile.GetComponent<BoxCollider>())
            {
                tile.gameObject.AddComponent<BoxCollider>();
            }
        }
    }
}
