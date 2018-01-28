﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool lockDown = false;
    private const float lockdownTimeSeconds = 10;
    private GameState _gamestate;
    public Phone phone;

    private float lockDownTimeRemaining;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject endScreen;

    //phone test
    public string application;
    public string author;
    public string message;

    public Text gameOverText;
    public Image gameOverScreen;

    public enum CardImage
    {
        CentralOne = 1,
        Explorer = 2,
        EuropeanExpress = 3,
        Vista = 4,
        PowerCard = 5
    }

    public enum HackStrength
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    enum GameState
    {
        StartScreen,
        Active,
        EndScreen
    }

    public enum WTFVoice
    {
        Male,
        Female
    }

    private void ChangeState(GameState gamestate)
    {
        _gamestate = gamestate;
        switch (gamestate)
        {
            case GameState.StartScreen:
                Time.timeScale = 0f;
                pausePanel.SetActive(false);
                titleScreen.SetActive(true);
                endScreen.SetActive(false);
                return;
            case GameState.Active:
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
                titleScreen.SetActive(false);
                endScreen.SetActive(false);
                SoundManager.instance.Init();
                return;
            case GameState.EndScreen:
                Time.timeScale = 0f;
                pausePanel.SetActive(false);
                titleScreen.SetActive(false);
                endScreen.SetActive(true);
                SoundManager.instance.Init();
                return;
        }
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _gamestate = new GameState();
        ChangeState(GameState.StartScreen);
        phone = GameObject.Find("Phone").GetComponent<Phone>();
    }

    void Update()
    {
        Notification(application, author, message);

        if (_gamestate != GameState.Active)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                Debug.Log("Pausing...");
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy)
            {
                Debug.Log("Resuming...");
                ContinueGame();
            }
        }

        if(lockDown)
        {
            lockDownTimeRemaining -= Time.deltaTime;
            if(lockDownTimeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);

        //Disable scripts that still work while timescale is set to 0
        SoundManager.instance.PauseMusic();
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);

        //enable the scripts again
        SoundManager.instance.ContinueMusic();
    }

    public void UpdateCashCount(int total)
    {
        phone.Money(total);
    }

    public void HackCard(int slot, bool safety, int total, CardImage card, HackStrength hack, int hackPercentage)
    {
        // Carrier bar
        phone.Safety(safety);
        phone.Money(total);

        // choice panel
        phone.Slot(slot);

        //panel
        phone.Progression(hackPercentage);
    }

    public void Notification(string fapplication, string fauthor, string fmessage)
    {
        phone.Notification(fapplication, fauthor, fmessage);
    }

    public bool isLockdown()
    {
        return lockDown;
    }

    public void Lockdown(WTFVoice voice)
    {
        if(!lockDown)
        {
            phone.Safety(!lockDown);
            lockDown = true;
            lockDownTimeRemaining = lockdownTimeSeconds;
        }
        LockdownTimer.instance.StartLockdown(voice);
        // TODO: SoundManager.instance.ToggleLockdown(lockdownAudioClip);
    }

    public void LockdownEnd()
    {
        phone.Safety(lockDown);
        lockDown = false;
    }

    public void ApplyDetectPenaltyDuringLockdown()
    {
        if (lockDown)
            lockDownTimeRemaining--;
    }

    public void EndGame(int cash)
    {
        ChangeState(GameState.EndScreen);
        LockdownEnd();
        if (cash == -1)
        {
            GameOver();
        }
        else
        {
            Win(cash);
        }
    }

    private void GameOver()
    {
        gameOverText.text = "You Got Tapped!";
        gameOverScreen.color = Color.red;
    }

    private void Win(int cash)
    {
        gameOverText.text = "Tapped $" + cash + "!";
        gameOverScreen.color = Color.blue;
    }

    public void StartGame()
    {
        ChangeState(GameState.Active);
    }

    public void UpdateMoney(int cash)
    {
        //update UI cash value with new total
    }

    public void UpdatePhone(int slotID, int cardID, float percentComplete, float signalStrength, bool justDrained)
    {
        //pass info to phone
    }


    public void UpdatePhone(int slotID, bool isEmpty)
    {
        //update slot slotID as empty
    }



    public void PlayerHasExit()
    {
        EndGame(phone.cashValue);
    }
    public void TakeOutPhone()
    {
        phone.showPhone = true;
        SoundManager.instance.PlayPhoneSound(true);
    }

    public void PutAwayPhone()
    {
        phone.showPhone = false;
        SoundManager.instance.PlayPhoneSound(false);
    }
}
