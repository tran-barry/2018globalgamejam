using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButton : MonoBehaviour
{
    public Button yourButton;

    void Start()
    {
        Debug.Log("Start button initialized");
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Load Level 1");
        SceneManager.LoadScene("Level1");
    }
}