using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsPaused { get; set; }

    public bool DialogActive { get; set; }

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        this.IsPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="paused">Active state for pause.  Specifying null will function as a toggle.</param>
    public void GamePause(bool? paused = null)
    {
        this.IsPaused = paused ?? !UIManager.Instance.PauseScreen.activeInHierarchy;
        UIManager.Instance.PauseScreen.SetActive(this.IsPaused);
        Time.timeScale = (this.IsPaused ? 0f : 1f);
    }
}
