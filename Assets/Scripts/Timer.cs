using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
   
    [SerializeField] private GameObject GameOverPanel;

    private const float TimeScalePaused = 0f;
    private const float TimeScaleNormal = 1f;

    private float timer;
    private bool   playing = false;

    void Start()
    {
      


    }

    void Update()
    {
        
    }

  //  private void ShowGameOver()
   // {
    //    if (GameOverPanel != null)
     //       GameOverPanel.SetActive(true);
//
   //     Time.timeScale = TimeScalePaused;
   //     Cursor.lockState = CursorLockMode.None;
   //     Cursor.visible = true;
  //  }

    public void RestartGame()
    {
        Time.timeScale = TimeScaleNormal;
        SceneManager.LoadScene("MainAriefScene");
    }

}