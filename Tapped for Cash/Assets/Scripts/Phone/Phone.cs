﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * CARRIER BAR
 *  Safety(bool) control the safety message in the carrierBar
 *  Money(int) control the total cash
 *  
 * ENEMIES PANEL
 *  Signal(signal) control the strenght of the signal with a range from 0 (all grey bar) to 3 (all green bar)
 *  Progression(progression) control the progression of money rob bar
 *  CashValue(cashValue) potential cash steal
 */

public class Phone : MonoBehaviour {

    public Sprite[] phoneImages;
    CarrierBar carrierBar;
    EnemyPanel[] enemyPanel = new EnemyPanel[3];
    public Sprite[] creditCard;
    Notification notification;


    [Header("Carrier bar")]
    public bool safety = true;
    public int money = 0;

    [Header("Enemy panel")]
    [Range(0,3)]
    public int signal;
    [Range(0, 100)]
    public int progression;
    public int cashValue;
    public bool hider;

    [Header("notification")]
    public bool startAnimation = false;

    public bool showPhone, phoneUp;
    private const float phoneSpeed = 10f;
    private const float maxHeight = 0f;
    private const float minHeight = -450f;

    // Use this for initialization
    void Start()
    {
        carrierBar = GameObject.Find("CarrierBar").GetComponent<CarrierBar>();
        enemyPanel[0] = GameObject.Find("EnemyPanel A").GetComponent<EnemyPanel>();
        enemyPanel[1] = GameObject.Find("EnemyPanel B").GetComponent<EnemyPanel>();
        enemyPanel[2] = GameObject.Find("EnemyPanel C").GetComponent<EnemyPanel>();
        notification = GameObject.Find("Notification").GetComponent<Notification>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        Safety(safety);
        Money(money);

        if (showPhone)
        {
            TakePhoneOut();
        }
        else
        {
            PutPhoneAway();
        }
	}
      

    // CARRIER BAR
    public void Safety(bool fSafety)
    {
        carrierBar.Safety(fSafety);
    }

    public void Money(int fMoney)
    {

        carrierBar.Money(fMoney);
    }

    // PANEL MODIFICATION


    public void CreditCard(int slot, int cardSlot)
    {
        //enemyPanel[slot].CreditCard(cardSlot);
    }

    public void Signal(int slot, int fSignal)
    {
        enemyPanel[slot].Signal(fSignal);
    }

    public void CardBrand(int slot, GameManager.CardImage crd)
    {
        Debug.LogWarning("credit: " + crd.ToString());
        enemyPanel[slot].creditCard.sprite = phoneImages[(int)crd];
    }

    public void Progression(int slot, int fProgression)
    {
        enemyPanel[slot].Progression(fProgression);
    }

    public void CashValue(int slot, int fCashValue)
    {
        enemyPanel[slot].CashValue(fCashValue);
    }

    public void ShowSlot(int slot, bool show)
    {
        enemyPanel[slot].ShowPanel(show);
    }



    // NOTIFICATION
    public void Notification(string application, string author, string message)
    {
        if (phoneUp)
        {
            notification.NotificationMessage(application, author, message);
        }
    }

    public void TakePhoneOut()
    {
        if (gameObject.transform.localPosition.y < maxHeight)
        {
            gameObject.transform.Translate(0, phoneSpeed, 0);
        }
        phoneUp = true;
    }

    public void PutPhoneAway()
    {
        if (gameObject.transform.localPosition.y > minHeight)
        {
            gameObject.transform.Translate(0, -phoneSpeed, 0);
        }
        phoneUp = false;
    }
}
