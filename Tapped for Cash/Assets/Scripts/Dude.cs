using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        // GLTODO: Call whatever UI starts the scanning process (OnTriggerStay2D does the actual scoring
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("dude trigger");
        if (collision.name == "BasicPlayer")
        {
            Player playa = collision.GetComponent<Player>();
            if (playa.IsScanning())
                Debug.Log("Player overlap start");

        }
    }
}
