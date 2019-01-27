using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class textControler : MonoBehaviour
{
    public float extra = 5;
    Text text;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time);


        if (Time.time > 0.0f+extra && Time.time < 2.0f + extra) {
            text.text = "Welcome back, <b>Home</b>.\n Global GameJam 2019";
            text.CrossFadeAlpha(0, 4, false);

        }
     


        if (Time.time > 7.0f + extra && Time.time < 11.0f + extra)
        {
            text.CrossFadeAlpha(1, 2, false);
            text.text = "Art, <b>Dora & Mónica</b>.";
        }
        if (Time.time > 11.0f + extra && Time.time < 13.0f + extra)
        {
            text.CrossFadeAlpha(0, 2, false);
        
        }


        if (Time.time > 13.0f + extra && Time.time < 16.0f + extra)
        {
            text.CrossFadeAlpha(1, 2, false);
            text.text = "Music SoundFX <b>David</b>.";
        }
        if (Time.time > 16.0f + extra && Time.time < 18.0f + extra)
        {
            text.CrossFadeAlpha(0, 2, false);

        }


        if (Time.time > 18.0f + extra && Time.time < 21.0f + extra)
        {
            text.CrossFadeAlpha(1, 2, false);
            text.text = "Programing <b>Bruno e Moisés </b>.";
        }
        if (Time.time > 21.0f + extra && Time.time < 23.0f + extra)
        {
            text.CrossFadeAlpha(0, 2, false);

        }
        if (Time.time > 23.0f + extra && Time.time<50)
        {
            text.CrossFadeAlpha(1, 2, false);
            text.text = "Original Ideia - Us. \n <b>Thank you for Playing!</b>";
        }

        if (Time.time > 50) {
            GameObject.Find("Panel").GetComponent<Image>().color = Color.black;
            text.text = "Press Enter To Play agin";
            if (Input.GetKeyDown(KeyCode.KeypadEnter)) {
                SceneManager.LoadScene(0); 
            }
        }

    }
}
