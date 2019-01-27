using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestorInicioLevel : MonoBehaviour
{
    public AudioSource aS;
    public AudioClip clip;
    public bool jaToquei = false;
    MusicController m;
    public GameObject g;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) { 
            if (!jaToquei) aS.PlayOneShot(clip);
            m = GameObject.Find("MusicControlller").GetComponent<MusicController>();
            m.audioSources[m.currentAudioSource].volume = 0.5f;
            jaToquei = true;
            Destroy(g);
        }
       

        
    }
    private void Update()
    {
        if (jaToquei)
        {
            m.audioSources[m.currentAudioSource].volume = 1.0f;
            Destroy(this);
        }
    }
}


