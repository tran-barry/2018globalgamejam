using UnityEngine;
using UnityEngine.UI;

public class LockdownTimer : MonoBehaviour
{
    public int TimeLeft = 0;
    public bool isEnabled = false;

    public static LockdownTimer instance = null;

    private Text m_text;
    private float m_leftTime;
    private const int StartTime = 10;
    private const float PenaltyAmount = 60f;

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

        m_text = GetComponent<Text>();
    }

    public void StartLockdown()
    {
        isEnabled = true;
        m_text.enabled = true;
        m_leftTime = GetInitialTime();
    }

    public void StopLockdown()
    {
        isEnabled = false;
        m_text.enabled = false;
    }

    public void ApplyPenalty()
    {
        m_leftTime -= PenaltyAmount;
    }

    private void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        if (m_leftTime > 0f)
        {
            //  Update countdown clock
            m_leftTime -= Time.deltaTime;
            TimeLeft = GetLeftSeconds();

            //  Show current clock
            if (m_leftTime > 0f)
            {
                m_text.text = TimeLeft.ToString("00");
            }
            else
            {
                //  The countdown clock has finished
                m_text.text = "00";
            }
        }

        if (m_leftTime <= 0f)
        {
            GameManager.instance.EndGame(-1);
        }
    }

    private float GetInitialTime()
    {
        return StartTime * 60f;
    }
    
    private int GetLeftSeconds()
    {
        return Mathf.FloorToInt(m_leftTime % 60f);
    }
}