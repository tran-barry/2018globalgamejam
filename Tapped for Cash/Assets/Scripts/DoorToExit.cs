using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToExit : MonoBehaviour {

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //GTFO

        }
    }


}
