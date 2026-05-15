using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName;
    [SerializeField] private string gameSceneName1;
    [SerializeField] private string gameSceneName2;

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        SceneManager.LoadScene(gameSceneName1);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(gameSceneName2); 
    }
}
