using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour {
    private TextMeshProUGUI application;
    private TextMeshProUGUI authorText;
    private TextMeshProUGUI message;
    Vector2 basePosition;
    Vector2 position;
    public bool goDown;

    // Use this for initialization
    void Start () {
        application = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        authorText = GameObject.Find("AuthorText").GetComponent<TextMeshProUGUI>();
        message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();

        basePosition = transform.position;
        position.y = 200;
        transform.Translate(0, 200, 0);
    }

    private void Update()
    {
        Movement(goDown);
    }

    public void NotificationMessage(string fApplication, string fAuthor, string fMessage)
    {
        application.text = fApplication;
        authorText.text = fAuthor;
        message.text = fMessage;
    }

    void Movement(bool activate)
    {
        int reachPosition;

        if (activate)
        {
            transform.Translate(0, 1, 0);
            goDown = false;
        }
    }
}
