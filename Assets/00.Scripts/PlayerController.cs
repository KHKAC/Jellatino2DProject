using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 position = transform.position;
        position.x += 0.1f;
        position.y -= 0.1f;
        transform.position = position;
    }
}
