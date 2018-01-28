using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
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

    public void Lockdown(AudioClip lockdownAudioClip)
    {
        // Max put your logic here for the phone

        LockdownTimer.instance.StartLockdown();
        SoundManager.instance.ToggleLockdown(lockdownAudioClip);
    }

    public void ApplyDetectPenaltyDuringLockdown()
    {

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
