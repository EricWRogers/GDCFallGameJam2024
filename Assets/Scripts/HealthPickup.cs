using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
public class HealthPickup : MonoBehaviour
{

    public int healthValue;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInParent<Health>().Heal(healthValue);
            Destroy(gameObject);
        }
    }
}
