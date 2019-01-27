using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyLightsController : MonoBehaviour
{
    public GameObject[] lights;
    private int currentLight;

    void Start()
    {
        currentLight = 0;
        StartCoroutine(NextLight());
    }

    IEnumerator NextLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.525f);
            lights[currentLight].SetActive(false);
            currentLight++;
            if (currentLight >= lights.Length)
                currentLight = 0;

            lights[currentLight].SetActive(true);
        }
    }
}
