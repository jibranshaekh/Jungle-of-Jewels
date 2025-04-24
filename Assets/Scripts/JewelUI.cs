using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class JewelUI : MonoBehaviour
{
    public static JewelUI instance; 
    public TextMeshProUGUI jewelText;
    public GameObject winScreen; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateJewelCounter(0, JewelCollected.totalJewels); //Calls JewelCollected script 
    }

    public void UpdateJewelCounter(int collected, int total)
    {
        jewelText.text = "Jewels Collected: " + collected + " / " + total; //Jewel text UI on game screen 
    }

    public void ShowWinScreen()
    {
    winScreen.SetActive(true);
    }

    public void RestartGame() 
    {
        JewelCollected.collectedJewels = 0; // Resets the jewel count from JewelCollected Script 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }



}