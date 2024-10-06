using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.Helper;
using SolarStudios;
public class HealthPickup : MonoBehaviour
{
    public AudioSource audio;
    public int healthValue;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInParent<Health>().Heal(healthValue);
            audio.Play();
            Destroy(gameObject, 2);
        }
    }
}
