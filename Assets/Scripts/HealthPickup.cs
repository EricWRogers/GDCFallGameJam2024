using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SolarStudios;
using UnityEngine.Events;
public class HealthPickup : MonoBehaviour
{
    public int healthValue;

    public UnityEvent onPickup;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPickup.Invoke();
            other.GetComponentInParent<Health>().Heal(healthValue);
            Destroy(gameObject, 2);
        }
    }
}
