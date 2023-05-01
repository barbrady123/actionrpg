using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        if (AudioManager.Instance != null)      Destroy(AudioManager.Instance.gameObject);
        if (UIManager.Instance != null)         Destroy(UIManager.Instance.gameObject);
        if (PlayerController.Instance != null)  Destroy(PlayerController.Instance.gameObject);
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
