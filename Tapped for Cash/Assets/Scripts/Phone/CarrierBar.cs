using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrierBar : MonoBehaviour
{
    Text safety;
    Text money;

    // Use this for initialization
    void Start ()
    {
        safety = GameObject.Find("SafeMessage").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Text>();
    }

    public void Safety (bool fSafety)
    {
        if (fSafety)
        {
            safety.text = "Safe";
            safety.color = Color.white;
        }
        else
        {
            safety.text = "Run!";
            safety.color = Color.red;
        } 
    }

    public void Money (int fMoney)
    {
        money.text = "$" + fMoney;
    }
}
