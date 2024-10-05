using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int dmg = 1;
    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Triggered by: " + col.gameObject.name + " with tag: " + col.gameObject.tag);
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy has hit: " + col.gameObject.name);
            col.gameObject.GetComponentInParent<Health>().Damage(dmg);
        }
    }
}
