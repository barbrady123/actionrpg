using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;

    public GameObject DeathAnimation;

    public int CurrentHP;
    public int MaxHP;

    public float InvincibilityDuration = 1f;

    public float _invincibilityTime;

    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        this.CurrentHP = this.MaxHP;
    }

    private void Update()
    {
        if (this.CurrentHP <= 0)
        {
            Dead();
        }

        if (_invincibilityTime > 0)
        {
            _invincibilityTime = Math.Max(_invincibilityTime - Time.deltaTime, 0);
        }
    }

    public int Damage(int dmg)
    {
        if (_invincibilityTime > 0f)
            return 0;

        int actualDmg = (this.CurrentHP >= dmg) ? dmg : this.CurrentHP;
        this.CurrentHP -= actualDmg;
        print($"Player took {actualDmg} damage...");
        _invincibilityTime = this.InvincibilityDuration;
        return actualDmg;
    }

    private void Dead()
    {
        Instantiate(this.DeathAnimation, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}