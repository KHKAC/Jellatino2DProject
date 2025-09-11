using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region SerializeField
    [SerializeField] float speed;
    [SerializeField] bool vertical;
    [SerializeField] float changeTime = 3.0f;
    #endregion

    #region private
    Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
        }
        rb2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
