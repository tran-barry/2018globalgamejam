using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPanel : MonoBehaviour {
    Image  creditCard;
    Image  signalLow;
    Image  signalMedium;
    Image  signalHigh;
    Slider progression;
    Text   cashValue;
    Image  hider;

    // Use this for initialization
    void Start ()
    {
        creditCard = transform.Find("CreditCard").GetComponent<Image>();
        signalLow = transform.Find("Signal/Low").GetComponent<Image>();
        signalMedium = transform.Find("Signal/Medium").GetComponent<Image>();
        signalHigh = transform.Find("Signal/High").GetComponent<Image>();
        progression = transform.Find("Progression").GetComponent<Slider>();
        cashValue = transform.Find("CashValue").GetComponent<Text>();
        hider = transform.Find("Hider").GetComponent<Image>();
    }

    public void Signal (int fSignal)
    {
        if (fSignal == 1)
        {
            signalLow.color = Color.green;
            signalMedium.color = Color.grey;
            signalHigh.color = Color.grey;
        }
        else if (fSignal == 2)
        {
            signalLow.color = Color.green;
            signalMedium.color = Color.green;
            signalHigh.color = Color.grey;
        }
        else if (fSignal == 3)
        {
            signalLow.color = Color.green;
            signalMedium.color = Color.green;
            signalHigh.color = Color.green;
        }
        else
        {
            signalLow.color = Color.grey;
            signalMedium.color = Color.grey;
            signalHigh.color = Color.grey;
        }
    }

    public void Progression(int fProgression)
    {
        progression.value = fProgression;
    }

    public void CashValue(int fCashValue)
    {
        cashValue.text = "$" + fCashValue;
    }

    public void Hider(bool fHider)
    {
        if (fHider)
        {
            hider.color = Color.white;
        }
        else
        {
            hider.color = Color.clear;
        }
    } 
}
