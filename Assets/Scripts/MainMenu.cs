using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void StartGame()
    {
        SceneInit.EntryTransition = null;
        SceneManager.LoadScene(Global.Scenes.Village);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
