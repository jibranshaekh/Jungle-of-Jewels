using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject startScreen;

    void Start()
    {
        Time.timeScale = 0f; // Pauses the game at start
        startScreen.SetActive(true);
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        Time.timeScale = 1f; // Once we press start the game begins
    }

}
