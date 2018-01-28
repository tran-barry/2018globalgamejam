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
    

    [Header("Carrier bar")]
    public bool safety;
    public int money;

    [Header("Panel choice")]
    [Range(0, 2)]
    public int enemyPanelControl;

    [Header("Enemy panel")]
    [Range(0,3)]
    public int signal;
    [Range(0, 100)]
    public int progression;
    public int cashValue;
    public bool hider;
    


    // Use this for initialization
    void Start()
    {
        carrierBar = GameObject.Find("CarrierBar").GetComponent<CarrierBar>();
        enemyPanel[0] = GameObject.Find("EnemyPanel A").GetComponent<EnemyPanel>();
        enemyPanel[1] = GameObject.Find("EnemyPanel B").GetComponent<EnemyPanel>();
        enemyPanel[2] = GameObject.Find("EnemyPanel C").GetComponent<EnemyPanel>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Safety(safety);
        Money(money);
        Signal(signal);
        Progression(progression);
        CashValue(cashValue);
        Hider(hider);
	}
    
    public void Safety(bool fSafety)
    {
        carrierBar.Safety(fSafety);
    }

    public void Money(int fMoney)
    {
        carrierBar.Money(fMoney);
    }

    public void Signal(int fSignal)
    {
        enemyPanel[enemyPanelControl].Signal(fSignal);
    }

    public void Progression(int fProgression)
    {
        enemyPanel[enemyPanelControl].Progression(fProgression);
    }

    public void CashValue(int fCashValue)
    {
        enemyPanel[enemyPanelControl].CashValue(fCashValue);
    }

    public void Hider(bool fHider)
    {
        enemyPanel[enemyPanelControl].Hider(fHider);
    } 
}
