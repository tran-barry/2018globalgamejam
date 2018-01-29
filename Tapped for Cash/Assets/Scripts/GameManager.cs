using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool lockDown = false;
    private const float lockdownTimeSeconds = 20;
    private GameState _gamestate;
    public Phone phone;
    public bool isScanning = false;

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
        None = 0,
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
            if(lockDownTimeRemaining < 0)
            {
                EndGame(-1);
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

    public bool isLockdown()
    {
        return lockDown;
    }

    public void Lockdown(WTFVoice voice)
    {
        Debug.Log("Lockdown start");
        if(!lockDown)
        {
            phone.Safety(!lockDown);
            lockDown = true;
            lockDownTimeRemaining = lockdownTimeSeconds;
        }
        LockdownTimer.instance.StartLockdown();
        SoundManager.instance.StartLockdownMusic(voice);
    }

    public void LockdownEnd()
    {
        Debug.Log("Lockdown End");
        phone.Safety(lockDown);
        lockDown = false;
        LockdownTimer.instance.StopLockdown();
        SoundManager.instance.StopLockdownMusic();
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
        gameOverText.text = "Tapped $" + cash + " (out of $3000)!";
        gameOverScreen.color = Color.blue;
    }

    public void StartGame()
    {
        ChangeState(GameState.Active);
    }

    public void UpdateCashCount(int total)
    {
        phone.money = total;
        SoundManager.instance.PlayCashSound();
    }

    public void HackCard(int slot, CardImage card, int cashValue, HackStrength hack, int hackPercentage)
    {
        phone.Signal(slot, (int)hack);
        phone.Progression(slot, hackPercentage);
        phone.CashValue(slot, cashValue);
        phone.CardBrand(slot, card);
        
    }

    public void Notification(string fapplication, string fauthor, string fmessage)
    {
        phone.Notification(fapplication, fauthor, fmessage);
    }

    public void ShowSlot(int slotID, bool isEmpty)
    {
        phone.ShowSlot(slotID, isEmpty);
    }

    public void PlayerHasExit()
    {
        EndGame(phone.money);
    }

    public void TakeOutPhone()
    {
        isScanning = true;
        phone.showPhone = true;
        SoundManager.instance.PlayPhoneSound(true);

        //TODO hook this up to actual scanning, not just taking phone out
        SoundManager.instance.ToggleScan();
    }

    public void PutAwayPhone()
    {
        isScanning = false;
        phone.showPhone = false;
        SoundManager.instance.PlayPhoneSound(false);

        //TODO hook this up to actual scanning, not just taking phone out
        SoundManager.instance.ToggleScan();
    }
}
