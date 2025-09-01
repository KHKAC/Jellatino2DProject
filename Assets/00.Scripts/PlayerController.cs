using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction LeftAction;
    void Start()
    {
        LeftAction.Enable();
    }

    void Update()
    {
        float horizontal = 0.0f;
        float vertical = 0.0f;
        if (LeftAction.IsPressed())
        {
            horizontal = -1.0f;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1.0f;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1.0f;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1.0f;
        }
        Debug.Log($"h: {horizontal} / v: {vertical}");
        Vector2 position = transform.position;
        position.x += 0.1f * horizontal;
        position.y += 0.1f * vertical;
        transform.position = position;
    }
}
