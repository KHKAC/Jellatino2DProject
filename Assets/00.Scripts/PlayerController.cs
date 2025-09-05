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
    #region public
    public InputAction MoveAction;
    [Range(MIN_HEALTH, MAX_HEALTH)] public int maxHealth = START_HEALTH;
    [Range(MIN_SPEED, MAX_SPEED)] public float MoveSpeed = START_SPEED;
    #endregion
    #region private
    int currentHealth;
    Rigidbody2D rb2D;
    Vector2 move;
    #endregion

    #region Method
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        MoveAction.Enable();
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        //Debug.Log(move);
    }


    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb2D.position + move * MoveSpeed * Time.deltaTime;
        rb2D.MovePosition(position);
    }

    void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, MIN_HEALTH, maxHealth);
        Debug.Log($"{currentHealth} / {maxHealth}");
    }
    #endregion
}
