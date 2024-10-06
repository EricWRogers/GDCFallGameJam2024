using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DualPickups : MonoBehaviour
{
    public UnityEvent onPickup;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            onPickup.Invoke();
            GameObject player = other.GetComponentInParent<PlayerMovement>().gameObject;
            player.GetComponent<PlayerMovement>().dual = true;
            player.GetComponent<PlayerMovement>().anim = null;

            player.GetComponentInChildren<RevolverGun>().gameObject.SetActive(false);
            //player.GetComponentInChildren<DualWeild>().gameObject.SetActive(true);
            Destroy(gameObject, 2);
        }
    }
}
