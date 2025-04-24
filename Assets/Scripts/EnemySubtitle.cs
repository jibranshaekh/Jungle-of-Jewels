using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubtitle : MonoBehaviour
{
    [TextArea]
    public string[] dialogLines;

    public float interval = 12f; // Interval is the time between dialogues appear duration is number of seconds and playerTriggerDistance controls the proximity of the player before dialogues trigger 
    public float duration = 3f;
    public float playerTriggerDistance = 10f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Fetches the player based off its tag assigned

        if (dialogLines.Length > 0 && player != null)
            StartCoroutine(SubtitleRoutine());
    }

    IEnumerator SubtitleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (player != null && SubtitleManager.instance.CanShowSubtitle()) //If the player is within the trigger distance the dialogues begin to start 
            {
                float distance = Vector3.Distance(transform.position, player.position);

                if (distance <= playerTriggerDistance)
                {
                    string line = dialogLines[Random.Range(0, dialogLines.Length)];
                    SubtitleManager.instance.ShowSubtitle(line, duration);
                }
            }
        }
    }
}