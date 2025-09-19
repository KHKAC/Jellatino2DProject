using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int increaseHealth;
    [SerializeField] AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"Object that entered the trigger {other}.");
        PlayerController controller = other.GetComponent<PlayerController>();

        if ((controller != null) && (controller.CurrentHealth < controller.maxHealth))
        {
            controller.ChangeHealth(increaseHealth);
            controller.PlaySound(collectedClip);
            Destroy(gameObject);
        }
    }
}
