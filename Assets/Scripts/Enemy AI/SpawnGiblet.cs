using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGiblet : MonoBehaviour
{
    public GameObject blood;
    public GameObject deathBlood;
    public Transform bleedLocation;
    public Transform dieLocation;

    public void BleedingOut()
    {
        GameObject gib = Instantiate(blood, bleedLocation.position, Quaternion.identity);
        Destroy(gib, 1.0f);
    }

    public void Died()
    {
        GameObject gib = Instantiate(deathBlood, dieLocation.position, Quaternion.identity);
        Destroy(gib, 1.0f);
    }
}
