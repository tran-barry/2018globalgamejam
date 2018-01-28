using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool lockDown = false;
    private GameState _gamestate;
    private Phone phone;
    
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject endScreen;

    public enum CardImage
    {
        CentralOne = 1,
        Explorer = 2,
        EuropeanExpress = 3,
        Vista = 4,
        PowerCard = 5
    }

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

        if (Input.GetKeyUp(KeyCode.L))
        {
            LockdownStart(false);
        }

        if(lockDown && LockdownTimer.instance.remainingTimeFloat <= 0f)
        {
            EndGame(-1);
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
        
    }

    public void hackCard(int slot, CardImage card, int hackStrenth, int hackPercentage)
    {
        // max
    }

    public bool isLockdown()
    {
        return lockDown;
    }

    public void LockdownStart(bool isFemale)
    {
        if(!lockDown)
        {
            phone.Safety(!lockDown);
            lockDown = true;
            LockdownTimer.instance.StartLockdown(isFemale);
        }
    }

    public void LockdownEnd()
    {
        phone.Safety(lockDown);
        lockDown = false;
        LockdownTimer.instance.StopLockdown();
    }

    public void ApplyDetectPenaltyDuringLockdown()
    {
        if (lockDown)
            LockdownTimer.instance.ApplyPenalty();
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


}
