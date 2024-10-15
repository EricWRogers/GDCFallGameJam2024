using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGiblet : MonoBehaviour
{
    public GameObject blood;
    public Transform bleedLocation;

    public void BleedingOut()
    {
        GameObject gib = Instantiate(blood, bleedLocation.position, Quaternion.identity);
        //Destroy(gib, 1.0f);
    }
}
