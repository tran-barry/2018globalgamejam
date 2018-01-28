using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarrierBar : MonoBehaviour
{
    TextMeshProUGUI safety;
    TextMeshProUGUI money;
    
    // Use this for initialization
    void Start ()
    {
        safety = GameObject.Find("SafeMessage").GetComponent<TextMeshProUGUI>();
        money = GameObject.Find("Money").GetComponent<TextMeshProUGUI>();
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
