using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI: MonoBehaviour
{
    public static GameUI instance;

    public GameObject gameOverScreen;
    public TextMeshProUGUI gameOverText; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowGameOverScreen(string message)
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);

            if (gameOverText != null)
            {
                gameOverText.text = "Game Over - " + message; //Message is adjusted for either being caught by enemy or out of bounds 
            }
        }

        Time.timeScale = 0f;
    }

    public void RestartGame() //asigned to restart button ui object 
    {
        Time.timeScale = 1f;
        
        JewelCollected.ResetJewelCount(); //Resets the jewel counter for when game is restarted 

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
