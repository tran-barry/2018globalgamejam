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
        if (fSafety) { safety.text = "Safe"; }
        else { safety.text = "Run!"; } 
    }

    public void Money (int fMoney)
    {
        money.text = "$" + fMoney;
    }
}
