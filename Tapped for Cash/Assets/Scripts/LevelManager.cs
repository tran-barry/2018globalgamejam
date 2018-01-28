using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public float levelCurrency = 3000;
    private List<TempCard> allSceneCards;


	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void InitializeCards()
    {
        GameObject[] cardObjects = GameObject.FindGameObjectsWithTag("Card");
        int cardCount = cardObjects.Length;

        allSceneCards = new List<TempCard>(cardCount);

        //get random amount
        int[] randomWeights = new int[cardCount];
        int totalWeight = 0;
        for (int i = 0; i < cardCount; ++i)
        {
            randomWeights[i] = Random.Range(3, 11);
            totalWeight += randomWeights[i];
        }

        float perWeight = levelCurrency / totalWeight;

        List<string> uniqueCard4Digits = new List<string>(cardCount);
        for (int i = 0; i < cardCount; ++i)
        {
            string random4Digits = Random.Range(0, 10) + "" +Random.Range(0, 10) + "" + Random.Range(0, 10) + "" + Random.Range(0, 10);
            if (uniqueCard4Digits.Contains(random4Digits))
            {
                --i;
            }
            else
            {
                uniqueCard4Digits[i] = random4Digits;
            }
        }

        for (int i = 0; i < cardCount; ++i)
        {
            cardObjects[i].GetComponent<TempCard>().InitializeCard(uniqueCard4Digits[i], randomWeights[i]*perWeight, Random.Range(0,5));
        }


    }
}
