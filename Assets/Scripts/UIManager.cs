using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider HealthSlider;
    public TMP_Text HealthText;

    public Slider StaminaSlider;
    public TMP_Text StaminaText;

    public TMP_Text CoinText;

    public GameObject PauseScreen;

    public GameObject FadeScreen;
    private Image _fadeImage;

    private float _fadeSpeed = 1.5f;
    public FadeStatus FadeStatus;

    public Slider BossHealthSlider;
    public TMP_Text BossHealthText;

    private EnemyHealthController _bossHealth;
    private int _bossMaxHP;

    public GameObject DeathScreen;


    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _fadeImage = this.FadeScreen.GetComponent<Image>();
        FadeStatus = FadeStatus.None;
    }

    void Update()
    {
        UpdateHealthDisplay();
        UpdateStaminaDisplay();
        UpdateCoinDisplay();
        UpdateBossHealthDisplay();
        UpdateFade();
    }

    public bool IsFading => FadeStatus != FadeStatus.None;

    public void UpdateFade()
    {
        if (FadeStatus == FadeStatus.None)
            return;

        float alpha = _fadeImage.color.a;

        if (FadeStatus == FadeStatus.ToBlack)
        {
            if (alpha < 1f)
            {
                alpha = Mathf.Min(alpha + (Time.deltaTime * _fadeSpeed), 1f);
                _fadeImage.SetAlpha(alpha);
            }
            else
            {
                FadeStatus = FadeStatus.None;
                SceneInit.LoadTransitionScene(FadeFromBlack);
            }
        }
        else if (FadeStatus == FadeStatus.FromBlack)
        {
            if (alpha > 0f)
            {
                alpha = Mathf.Max(alpha - (Time.deltaTime * _fadeSpeed), 0f);
                _fadeImage.SetAlpha(alpha);
            }
            else
            {
                FadeStatus = FadeStatus.None;
                this.FadeScreen.SetActive(false);
            }
        }
    }

    // This is not a good way to do it, use the ScriptableObject variables thing...
    public void UpdateHealthDisplay()
    {
        int currentHP = PlayerHealthController.Instance.CurrentHP;
        int maxHP = PlayerHealthController.Instance.MaxHP;

        this.HealthText.text = $"{currentHP} / {maxHP}";
        this.HealthSlider.minValue = 0;
        this.HealthSlider.maxValue = maxHP;
        this.HealthSlider.value = currentHP;
    }

    public void SetBoss(EnemyHealthController bossHealth, string name)
    {
        _bossHealth = bossHealth;
        _bossMaxHP = _bossHealth?.CurrentHP ?? 0;
        this.BossHealthText.text = name;
    }

    public void UpdateBossHealthDisplay()
    {
        if (_bossHealth == null)
        {
            this.BossHealthSlider.gameObject.SetActive(false);
        }
        else
        {
            this.BossHealthSlider.gameObject.SetActive(true);
            this.BossHealthSlider.maxValue = _bossMaxHP;
            this.BossHealthSlider.minValue = 0f;
            this.BossHealthSlider.value = _bossHealth.CurrentHP;
        }
    }

    public void UpdateStaminaDisplay()
    {
        this.StaminaText.text = PlayerController.Instance.CurrentStamina.ToString();
        this.StaminaSlider.value = PlayerController.Instance.CurrentStamina;
        this.StaminaSlider.minValue = 0;
        this.StaminaSlider.maxValue = PlayerController.Instance.MaxStamina;
    }

    public void UpdateCoinDisplay()
    {
        this.CoinText.text = PlayerController.Instance.Coins.ToString();
    }

    public void ResumeGame()
    {
        GameManager.Instance.GamePause(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Global.Scenes.MainMenu);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FadeToBlack()
    {
        if (FadeStatus != FadeStatus.None)
            return;

        FadeStatus = FadeStatus.ToBlack;
        _fadeImage.SetAlpha(0f);
        this.FadeScreen.SetActive(true);
    }

    public void FadeFromBlack()
    {
        if (FadeStatus != FadeStatus.None)
            return;

        FadeStatus = FadeStatus.FromBlack;
        _fadeImage.SetAlpha(1f);
        this.FadeScreen.SetActive(true);
    }
}
