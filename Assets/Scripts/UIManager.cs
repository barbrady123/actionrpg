using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider HealthSlider;
    public TMP_Text HealthText;

    public Slider StaminaSlider;
    public TMP_Text StaminaText;

    public TMP_Text CoinText;

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateHealthDisplay();
        UpdateStaminaDisplay();
        UpdateCoinDisplay();
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
}
