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
}
