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
    [Header("Other")]
    public CarrierBar carrierBar;
    public EnemyPanel[] enemyPanel = new EnemyPanel[3];
    

    [Header("Test Variable")]
    public bool safety;
    public int money;

    [Range(0, 2)]
    public int enemyPanelControl;
    [Range(0,3)]
    public int signal;
    [Range(0, 100)]
    public int progression;
    public int cashValue;
    


    // Use this for initialization
    void Start()
    {
        CarrierBar carrierBar = transform.GetComponent<CarrierBar>();
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
	}

    void Safety(bool fSafety)
    {
        carrierBar.Safety(fSafety);
    }

    void Money(int fMoney)
    {
        carrierBar.Money(fMoney);
    }

    void Signal(int fSignal)
    {
        enemyPanel[enemyPanelControl].Signal(fSignal);
    }

    void Progression(int fProgression)
    {
        enemyPanel[enemyPanelControl].Progression(fProgression);
    }

    void CashValue(int fCashValue)
    {
        enemyPanel[enemyPanelControl].CashValue(fCashValue);
    }
}
