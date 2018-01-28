using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCard : MonoBehaviour {
    static int cardCount = 0;
    private int m_id;
    public int ID { get { return m_id; } }
    private GameManager.CardImage cardImage = GameManager.CardImage.CentralOne;
    public GameManager.CardImage CardImage { get { return cardImage; } }
    private string cardNumber = string.Empty;
    public string CardNumber { get { return cardNumber; } }
    private bool m_collected = false;
    public bool Collected { get { return m_collected; } set { m_collected = value; } }
    private float m_cash;
    public float Cash { get { return m_cash; } }
    private GameManager.CardImage m_CardImage = GameManager.CardImage.PowerCard;
    public GameManager.CardImage CardBrand { get { return m_CardImage; } }



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
        m_CardImage = (GameManager.CardImage)brandID;
    }
}
