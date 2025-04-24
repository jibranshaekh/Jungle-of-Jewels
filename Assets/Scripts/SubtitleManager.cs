using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleManager : MonoBehaviour // Manages which diallgues appear on the screen and ensures that their are no dialogue clashes 
{
    public static SubtitleManager instance;

    public TextMeshProUGUI subtitleText;
    public GameObject subtitleObject;

    private Coroutine currentRoutine;
    private float cooldown = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime; //updates cool down counter per frame
    }

    public bool CanShowSubtitle() //Allows for subtitle to be shown, called by enemies 
    {
        return cooldown <= 0f;
    }

    public void ShowSubtitle(string line, float duration = 3f) //Displays line for 3 seconds
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(DisplaySubtitle(line, duration));
    }

    IEnumerator DisplaySubtitle(string line, float duration) //handles both the cool down and when the line is displayed
    {
        subtitleText.text = line;
        subtitleObject.SetActive(true);
        cooldown = duration + 1f; 

        yield return new WaitForSeconds(duration);

        subtitleObject.SetActive(false);
        subtitleText.text = "";
        currentRoutine = null;
    }
}
