using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EasyTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public bool playing = false;
    private float timer = 10f;

    public GameObject gameOverPanel;

    void Start()
    {
        if  (gameOverPanel != null)
            gameOverPanel.SetActive(false);
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
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}