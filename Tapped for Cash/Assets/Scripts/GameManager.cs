using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private bool lockDown = false;
    private const float lockdownTimeSeconds = 10;
    private float lockDownTimeRemaining;
    [SerializeField] private GameObject pausePanel;

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
        pausePanel.SetActive(false);
    }

    void Update()
    {
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
}
