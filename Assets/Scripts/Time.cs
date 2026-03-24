using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EasyTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public bool playing = false;
    private float timer = 300f;

    void Update()
    {
        if (playing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                playing = false;
            }
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}