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
    const float LAUNCH_UP = 0.5f;
    const float LAUNCH_FORCE = 300.0f;
    const float RAY_UP = 0.2f;
    const float RAY_DISTANCE = 1.5f;
    #endregion

    #region Public
    public InputAction MoveAction;
    public InputAction talkAction;
    [Range(MIN_HEALTH, MAX_HEALTH)] public int maxHealth = START_HEALTH;
    [Range(MIN_SPEED, MAX_SPEED)] public float MoveSpeed = START_SPEED;
    public float timeInvincible = 2.0f;
    public float timeHealingZone = 1.5f;
    public GameObject projPrefab;
    #endregion

    #region Property
    // public int CurrentHealth
    // {
    //     get { return currentHealth; }
    // }
    public int CurrentHealth => currentHealth;
    #endregion

    #region Private
    int currentHealth;
    Rigidbody2D rb2D;
    Vector2 move;
    bool isInvincible;
    bool isHealing;
    float damageCooldown;
    float healingCooldown;
    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
    #endregion

    #region Method
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        talkAction.Enable();
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        // if((move.x != 0.0f) || (move.y != 0.0f))
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }
        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", move.magnitude);

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

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            FindFriend();
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
            animator.SetTrigger("Hit");
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
        UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projObject = Instantiate(projPrefab, rb2D.position + Vector2.up * LAUNCH_UP, Quaternion.identity);
        Projectile proj = projObject.GetComponent<Projectile>();
        proj.Launch(moveDirection, LAUNCH_FORCE);
        animator.SetTrigger("Launch");
    }

    void FindFriend()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rb2D.position + Vector2.up * RAY_UP, moveDirection, RAY_DISTANCE, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            Debug.Log($"Raycast Hit: {hit.collider.gameObject}");
        }
    }
    #endregion
}
