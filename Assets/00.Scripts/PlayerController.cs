using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public InputAction MoveAction;
    Rigidbody2D rb2D;
    Vector2 move;
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        rb2D = GetComponent<Rigidbody2D>();
        MoveAction.Enable();
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
    }


    void FixedUpdate()
    {
        Vector2 position = (Vector2)rb2D.position + move * MoveSpeed * Time.deltaTime;
        rb2D.MovePosition(position);
    }
}
