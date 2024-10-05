using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int dmg = 1;
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            // Calculate the hit direction
            Vector3 hitDir = col.gameObject.transform.position - gameObject.transform.position;

            // Apply damage to the player
            col.gameObject.GetComponent<Health>().Damage(dmg);
        }
    }
}
