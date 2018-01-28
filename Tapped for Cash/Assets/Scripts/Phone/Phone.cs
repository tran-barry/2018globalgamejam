using System.Collections;
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
    
    CarrierBar carrierBar;
    EnemyPanel[] enemyPanel = new EnemyPanel[3];
    Notification notification;


    [Header("Carrier bar")]
    public bool safety;
    public int money = 0;

    [Header("Panel choice")]
    [Range(0, 2)]
    public int slot;

    [Header("Enemy panel")]
    [Range(0,3)]
    public int signal;
    [Range(0, 100)]
    public int progression;
    public int cashValue;
    public bool hider;

    public bool showPhone;
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
        Signal(signal);
        Progression(progression);
        
        CashValue(cashValue);

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


    // ENEMY PANEL SELECTION
    public void Slot(int enemyControl)
    {
        slot = enemyControl;
    }


    // PANEL MODIFICATION
    public void Signal(int fSignal)
    {
        enemyPanel[slot].Signal(fSignal);
    }

    public void Progression(int fProgression)
    {
        enemyPanel[slot].Progression(fProgression);
    }

    public void CashValue(int fCashValue)
    {
        enemyPanel[slot].CashValue(fCashValue);
    }





    // NOTIFICATION
    public void Notification(string application, string author, string message)
    {
        notification.NotificationMessage(application, author, message);
    }

    public void TakePhoneOut()
    {
        if (gameObject.transform.localPosition.y < maxHeight)
        {
            gameObject.transform.Translate(0, phoneSpeed, 0);
        }
    }

    public void PutPhoneAway()
    {
        if (gameObject.transform.localPosition.y > minHeight)
        {
            gameObject.transform.Translate(0, -phoneSpeed, 0);
        }
    }
}
