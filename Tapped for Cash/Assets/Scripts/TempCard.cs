using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempCard : MonoBehaviour {
    enum Brand { PowerCard, EuroExpress, Explore, Vista, CentralOne}

    static int cardCount = 0;
    private int m_id;
    public int ID { get { return m_id; } }
    private string cardNumber = string.Empty;
    public string CardNumber { get { return cardNumber; } }
    private bool m_collected = false;
    public bool Collected { get { return m_collected; } }


	// Use this for initialization
	void Start () {
        InitializeCard("1234");
        Debug.Log(cardNumber);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //functionality goes elsewhere eventually
    private void InitializeCard(string finalFourDigits)
    {
        m_id = cardCount;
        ++cardCount;
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                cardNumber += "" + Random.Range(0, 10);
            }
            cardNumber += " ";
        }
        cardNumber += finalFourDigits;
    }
}
