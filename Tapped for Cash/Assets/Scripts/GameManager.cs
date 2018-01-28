using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool lockDown = false;
    private const float lockdownTimeSeconds = 10;
    private GameState _gamestate;

    private float lockDownTimeRemaining;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject endScreen;

    enum GameState
    {
        StartScreen,
        Active,
        EndScreen
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

    public bool isLockdown()
    {
        return lockDown;
    }

    public void Lockdown(AudioClip lockdownAudioClip)
    {
        // Max put your logic here for the phone
        if(!lockDown)
        {
            lockDown = true;
            lockDownTimeRemaining = lockdownTimeSeconds;
        }
        LockdownTimer.instance.StartLockdown();
        SoundManager.instance.ToggleLockdown(lockdownAudioClip);
    }

    public void LockdownEnd()
    {
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
        LockdownTimer.instance.StopLockdown();
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

    }

    private void Win(int cash)
    {

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
}
