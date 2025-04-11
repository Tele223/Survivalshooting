using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammoAmount = 30;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("N");
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooting = other.GetComponentInChildren<PlayerShooting>();
            if (shooting != null)
            {
                shooting.AddAmmo(ammoAmount);
            }

            Destroy(gameObject);
        }
    }
}

