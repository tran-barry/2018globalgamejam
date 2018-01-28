using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour {
    public TextMeshProUGUI authorText;

    // Use this for initialization
    void Start () {
        authorText = GameObject.Find("AuthorText").GetComponent<TextMeshProUGUI>();
    }
	
	// Update is called once per frame
	void Update () {
        authorText.text = "test";
	}
}
