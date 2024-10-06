using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBats : MonoBehaviour
{
    public float timer = 5.0f;
    public GameObject bat;
    private float bulletTime;
    public Transform spawnPoint;

    //If I have time I add audio source
    void Start()
    {

    }

    void Update()
    {
        ShootAtPlayer();
    }
    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(bat, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Invoke("Delay", .08f);
    }
}
