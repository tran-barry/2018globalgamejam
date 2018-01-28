using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {
    public enum CardImage
    {
        CentralOne = 1,
        Explorer = 2,
        EuropeanExpress = 3,
        Vista = 4,
        PowerCard = 5
    }

    public string id = "4511 1231 1231 1321";
    public CardImage card;
    public int CashValue = 50;
    public bool isDrained;

	// Use this for initialization
	void Start () {

    }

    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
