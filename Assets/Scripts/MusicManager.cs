using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource defaultMusicSource;   //For background music
    public AudioSource proximityMusicSource; //For jewels
    public AudioSource chaseMusicSource;     // For chase from enemies 

    public float fadeSpeed = 2f; //Speed of music transition 

    private enum MusicState { Default, NearJewel, Chased }  //Variables to define which state music is in the script
    private MusicState currentState = MusicState.Default;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        //Sets up for only the background music to play on start 
        defaultMusicSource.volume = 1f;
        defaultMusicSource.loop = true;
        defaultMusicSource.Play();

        proximityMusicSource.volume = 0f;
        proximityMusicSource.loop = true;
        proximityMusicSource.Play();

        chaseMusicSource.volume = 0f;
        chaseMusicSource.loop = true;
        chaseMusicSource.Play();
    }

    void Update()
    {
        float targetDefault = 0f;
        float targetProximity = 0f;
        float targetChase = 0f;

        switch (currentState)
        //Sets which audio to source at which state 
        {
            case MusicState.Default:
                targetDefault = 1f;
                break;
            case MusicState.NearJewel:
                targetProximity = 1f;
                break;
            case MusicState.Chased:
                targetChase = 1f;
                break;
        }

        defaultMusicSource.volume = Mathf.MoveTowards(defaultMusicSource.volume, targetDefault, fadeSpeed * Time.deltaTime); // Handles smooth fade speed 
        proximityMusicSource.volume = Mathf.MoveTowards(proximityMusicSource.volume, targetProximity, fadeSpeed * Time.deltaTime);
        chaseMusicSource.volume = Mathf.MoveTowards(chaseMusicSource.volume, targetChase, fadeSpeed * Time.deltaTime);
    }

    public void SetChased(bool isChased)
    {
        if (isChased)
        {
            currentState = MusicState.Chased;
        }
        else if (currentState == MusicState.Chased)
        {
            currentState = MusicState.Default; //Resets to default once chase has ended 
        }
    }

    public void SetProximityActive(bool isNearJewel)
    {
        if (currentState == MusicState.Chased) return; //Only switches to this music if player is not being chased as well

        currentState = isNearJewel ? MusicState.NearJewel : MusicState.Default; //Resets to default is no longer near a jewel 
    }
}
