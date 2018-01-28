using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCard : MonoBehaviour {
    public enum Brand { PowerCard, EuroExpress, Explore, Vista, CentralOne }

    static int cardCount = 0;
    private int m_id;
    public int ID { get { return m_id; } }
    private string cardNumber = string.Empty;
    public string CardNumber { get { return cardNumber; } }
    private bool m_collected = false;
    public bool Collected { get { return m_collected; } set { m_collected = value; } }
    private float m_cash;
    public float Cash { get { return m_cash; } }
    private Brand m_brand = Brand.PowerCard;
    public Brand CardBrand { get { return m_brand; } }



	// Use this for initialization
	void Start () {
        //InitializeCard("1234");
        Debug.Log(cardNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //functionality goes elsewhere eventually
    public void InitializeCard(string finalFourDigits, float cash,int brandID)
    {
        m_id = cardCount;
        ++cardCount;
        
        cardNumber = "**** **** **** " + finalFourDigits;
        m_cash = cash;
        m_brand = (Brand)brandID;
    }
}
