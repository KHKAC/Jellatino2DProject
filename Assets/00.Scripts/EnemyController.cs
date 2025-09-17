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
    Animator animator;
    Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    bool broken = true;
    #endregion

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            direction = -direction;
            timer = changeTime;
            //vertical = Random.Range(0, 100) > 50 ? true : false;
            if (direction > 0)
            {
                vertical = Random.Range(0, 100) > 49;
            }
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }

        Vector2 position = rb2d.position;
        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }
        rb2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        // 적이 파괴되는 경우는 아래 주석을 사용
        // Destroy(gameObject);
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
