using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude_Detection : MonoBehaviour {

    const int LockdownTimeSeconds = 10;
    const int suspicionTimeInterval = 1;

    int suspicionLevel = 0; // 1-3 = # of exclamation marks, 4 = WTF (too late), 5 = penalty

    float startSuspicion = 0.0f;

    GameObject A1;
    GameObject A2;
    GameObject A3;
    GameObject WTF;

    // Use this for initialization
    void Start()
    {
        A1 = transform.Find("A1").gameObject;
        A2 = transform.Find("A2").gameObject;
        A3 = transform.Find("A3").gameObject;
        WTF = transform.Find("WTF").gameObject;
        UpdateSuspicion(true);
    }

    // Update is called once per frame
    //void Update () {

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.instance.isLockdown())
        {
            GameManager.instance.ApplyDetectPenaltyDuringLockdown();
            return;
        }
        if (!GameManager.instance.isScanning)
            return;
        startSuspicion = 0.0f;
        suspicionLevel = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (startSuspicion < 4 && suspicionLevel < 4)
        {
            startSuspicion = 0.0f;
            suspicionLevel = 0;
            UpdateSuspicion(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager.instance.isScanning)
            return;
        float currentSuspicion = startSuspicion;
        startSuspicion += Time.deltaTime;
        //Debug.Log("sus: " + startSuspicion.ToString());
        if ((int)startSuspicion > (int)currentSuspicion) // Went over an integer boundary
            UpdateSuspicion(false);
    }

    private void UpdateSuspicion(bool clear)
    {
        if(clear)
        {
            A1.SetActive(false);
            A2.SetActive(false);
            A3.SetActive(false);
            WTF.SetActive(false);
        }
        if (startSuspicion >= 5)
        {
            suspicionLevel = 5;
            return;
        }
        else if (startSuspicion >= 4)
        {
            suspicionLevel = 4;
            A1.SetActive(false);
            A2.SetActive(false);
            A3.SetActive(false);
            WTF.SetActive(true);
            GameManager.instance.Lockdown(GameManager.WTFVoice.Male);
        }
        else if (startSuspicion >= 3)
        {
            Debug.Log("syslevel: 3");
            suspicionLevel = 3;
            A3.SetActive(true);
        }
        else if (startSuspicion >= 2)
        {
            Debug.Log("syslevel: 2");
            suspicionLevel = 2;
            A2.SetActive(true);
        }
        else if (startSuspicion >= 1)
        {
            Debug.Log("syslevel: 1");
            suspicionLevel = 1;
            A1.SetActive(true);
        }
    }
}
