using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TMP_Text TimerText;
    public bool playing = false;
    private float timer = 10f;

    public GameObject GameOverPanel;

    void Start()
    {
        if  (GameOverPanel != null)
            GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (playing)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                timer = 0f;
                playing = false;
                ShowGameOver();
            }

            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void ShowGameOver()
    {
        if (GameOverPanel != null) GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainAriefScene");
      
    }
}