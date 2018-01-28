using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour {
    private TextMeshProUGUI application;
    private TextMeshProUGUI authorText;
    private TextMeshProUGUI message;
    private bool getTime;
    private float time;

    // Use this for initialization
    void Start () {
        application = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        authorText = GameObject.Find("AuthorText").GetComponent<TextMeshProUGUI>();
        message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    public void NotificationMessage(string fApplication, string fAuthor, string fMessage)
    {
        application.text = fApplication;
        authorText.text = fAuthor;
        message.text = fMessage;
    }
}
