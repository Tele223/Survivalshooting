using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealBox : MonoBehaviour
{
    public int healAmount = 25;
    public Action onHealUsed; // เอาไว้แจ้ง HealManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.currentHealth < playerHealth.startingHealth)
            {
                playerHealth.Heal(healAmount);

                onHealUsed?.Invoke(); // แจ้งว่า Heal ถูกใช้
                Destroy(gameObject);
            }
        }
    }
}
