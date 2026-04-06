using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private const float TimeScaleNormal = 1f;
    public void RestartGame()
    {
        Debug.Log("Button Clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
