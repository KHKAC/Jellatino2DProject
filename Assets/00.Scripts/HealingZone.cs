using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    [SerializeField] int increaseHealth;
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"Object that entered the trigger {other}.");
        PlayerController controller = other.GetComponent<PlayerController>();

        if ((controller != null) && (controller.CurrentHealth < controller.maxHealth))
        {
            controller.ChangeHealth(increaseHealth);
        }
    }
}