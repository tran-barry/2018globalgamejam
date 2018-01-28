using UnityEngine;
using UnityEngine.UI;

public class LockdownTimer : MonoBehaviour
{
    public int TimeLeft = 0;
    public bool isEnabled = false;

    public static LockdownTimer instance = null;

    private Text displayText;
    public float remainingTimeFloat;
    private const int InitialTime = 10;
    private const float PenaltyAmount = 1;

    private void Awake()
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

        displayText = GetComponent<Text>();
    }

    public void StartLockdown(GameManager.WTFVoice voice)
    {
        isEnabled = true;
        displayText.enabled = true;
        remainingTimeFloat = GetInitialTime();
        SoundManager.instance.StartLockdownMusic(voice);
    }

    public void StopLockdown()
    {
        isEnabled = false;
        displayText.enabled = false;
        SoundManager.instance.StopLockdownMusic();
    }

    public void ApplyPenalty()
    {
        remainingTimeFloat -= PenaltyAmount;
    }

    private void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        if (remainingTimeFloat > 0f)
        {
            //  Update countdown clock
            remainingTimeFloat -= Time.deltaTime;
            TimeLeft = GetLeftSeconds();

            //  Show current clock
            if (remainingTimeFloat > 0f)
            {
                displayText.text = TimeLeft.ToString("00");
            }
            else
            {
                //  The countdown clock has finished
                displayText.text = "00";
            }
        }
    }

    private float GetInitialTime()
    {
        return InitialTime;
    }
    
    private int GetLeftSeconds()
    {
        return (int) remainingTimeFloat;
    }
}