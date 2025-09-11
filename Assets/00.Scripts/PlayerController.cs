using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Const
    const int MIN_HEALTH = 0;
    const int MAX_HEALTH = 20;
    const int START_HEALTH = 5;
    const float MIN_SPEED = 1.0f;
    const float MAX_SPEED = 10.0f;
    const float START_SPEED = 3.0f;
    #endregion

    #region Public
    public InputAction MoveAction;
    [Range(MIN_HEALTH, MAX_HEALTH)] public int maxHealth = START_HEALTH;
    [Range(MIN_SPEED, MAX_SPEED)] public float MoveSpeed = START_SPEED;
    public float timeInvincible = 2.0f;
    public float timeHealingZone = 1.5f;
    #endregion

    #region Property
    public int CurrentHealth
    {
        get { return currentHealth; }
    }
    #endregion

    #region Private
    int currentHealth;
    Rigidbody2D rb2D;
    Vector2 move;
    bool isInvincible;
    bool isHealing;
    float damageCooldown;
    float healingCooldown;
    #endregion

    #region Method
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        Debug.Log($"{currentHealth} / {maxHealth}");
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
        if (isHealing)
        {
            healingCooldown -= Time.deltaTime;
            if (healingCooldown < 0)
            {
                isHealing = false;
            }
        }
    }


    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb2D.position + move * MoveSpeed * Time.deltaTime;
        rb2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        if (isHealing)
        {
            return;
        }
        else
        {
            isHealing = true;
            healingCooldown = timeHealingZone;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, MIN_HEALTH, maxHealth);
        // Debug.Log($"{currentHealth} / {maxHealth}");
        UIHandler.instance.SetHealthValue(currentHealth / (float) maxHealth);
    }
    #endregion
}
