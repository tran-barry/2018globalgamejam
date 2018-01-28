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
    public bool goDown, goRight, timeCheck, start;
    int speedDown, speedRight, reachScreen = 0, reachLeft = 0;
    float timeLeft;

    // Use this for initialization
    void Start () {
        application = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        authorText = GameObject.Find("AuthorText").GetComponent<TextMeshProUGUI>();
        message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();

        goRight = false;
        timeCheck = false;
        start = false;
        goDown = false;

        basePosition = transform.position;
        print(basePosition);
        transform.Translate(0, 200, 0);

        speedDown = -10;
        speedRight = 40;
    }

    private void Update()
    {
        if (start)
        {
            basePosition = transform.position;
            print(basePosition);
            start = false;
        } 

        Movement();
    }

    public void NotificationMessage(string fApplication, string fAuthor, string fMessage)
    {
        application.text = fApplication;
        authorText.text = fAuthor;
        message.text = fMessage;
        goDown = true;
        start = true;
    }

    void Movement()
    {
        if (goDown)
        {
            transform.Translate(0, speedDown, 0);
            reachScreen = reachScreen + speedDown;
        }

        if (reachScreen <= -200)
        {
            goDown = false;

            if (!timeCheck)
            {
                timeLeft = Time.time + 3;
                timeCheck = true;
            }

            if (Time.time >= timeLeft)
            {
                
                goRight = true;
            }
        }

        if (goRight)
        {
            transform.Translate(speedRight, 0, 0);
            reachLeft = speedRight + reachLeft;
        }

        if (transform.position.x >= basePosition.x + 500)
        {
            transform.position = basePosition;
            goRight = false;
            timeCheck = false;
            reachScreen = 0;
        }
    }
}
