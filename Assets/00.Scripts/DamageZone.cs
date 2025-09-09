using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int decreaseHealth;
    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log($"Object that entered the trigger {other}.");
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(-decreaseHealth);
        }
    }
}
