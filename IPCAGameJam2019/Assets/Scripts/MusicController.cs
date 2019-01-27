using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Transform[] waypoints;

    AudioSource[] audioSources;
    int currentAudioSource;
    int oldAudioSource;
    Transform player;

    float startTime;
    float timer;

    float initialTime;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        currentAudioSource = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        oldAudioSource = currentAudioSource;
        if (player.position.x < waypoints[waypoints.Length - 1].position.x)
        {
            currentAudioSource = waypoints.Length - 1;
        }
        else
        {
            for (int i = 0; i < waypoints.Length - 1; i++)
            {
                if (player.position.x <= waypoints[i].position.x && player.position.x > waypoints[i + 1].position.x)
                {
                    currentAudioSource = i;
                    break;
                }
            }
        }

        if (!audioSources[currentAudioSource].isPlaying)
        {
            audioSources[currentAudioSource].Play();
            //CrossFade();
        }
    }

    /*void CrossFade()
    {
        Fade(audioSources[currentAudioSource], true);
        Fade(audioSources[oldAudioSource], false);
    }

    //IEnumerator Fade(AudioSource audio, bool fadeIn)
    void Fade(AudioSource audioIn, AudioSource audioOut)
    {
        startTime = Time.time;
        float startValue = 0.0f;
        float endValue = 1.0f - startValue;
        if ()
        {
            float elapsed = Time.time - startTime;
            audio.volume = Mathf.Lerp(startValue, endValue, elapsed);

            if (audio.volume == endValue)
            {
                if (!fadeIn) audio.Stop();
            }
        }
    }*/
}
