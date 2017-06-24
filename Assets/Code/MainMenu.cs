using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string m_levelName;


    public void StartGame()
    {
        SceneManager.LoadScene(m_levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
