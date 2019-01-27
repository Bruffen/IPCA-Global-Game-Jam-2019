using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Transform[] waypoints;

    public AudioSource[] audioSources;
    public int currentAudioSource;
    int oldAudioSource;
    int fadingOutSource;
    Transform player;

    float timer;
    bool fading;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

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

        audioSources[currentAudioSource].Play();
        audioSources[currentAudioSource].volume = 1.0f;
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

        if (currentAudioSource != oldAudioSource)
        {
            if (!audioSources[currentAudioSource].isPlaying)
            {
                audioSources[currentAudioSource].Play();
            }
            fadingOutSource = oldAudioSource;
            fading = true;
            timer = 0.0f;
        }

        if (fading) Fade(audioSources[currentAudioSource], audioSources[fadingOutSource]);
    }

    void Fade(AudioSource audioIn, AudioSource audioOut)
    {
        timer += Time.deltaTime;

        audioIn.volume = Mathf.Lerp(0.0f, 1.0f, timer);
        audioOut.volume = Mathf.Lerp(1.0f, 0.0f, timer);

        if (timer > 1.0f)
        {
            audioOut.Stop();
            audioIn.volume = 1.0f;
            fading = false;
        }
    }
}
