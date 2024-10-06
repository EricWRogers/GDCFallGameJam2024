using SolarStudios;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DualWeild : GunBase
{
    public UnityEvent OnShootEvent;
    public UnityEvent OnReloadEvent;
    public TMP_Text text;

    public GameObject secondFirePoint;
    public ParticleSystem secondEffect;


    public override void OnRayHit()
    {
        OnShootEvent.Invoke();
        //We aint using these.
    }

    public override void OnReload()
    {
        OnReloadEvent.Invoke();
    }

    public override void OnShoot()
    {
        Instantiate(bulletPrefab, secondFirePoint.transform.position, Quaternion.identity);
        if(currentAmmo !=0)
        {
            currentAmmo--;
            
        }
        
        
        OnShootEvent.Invoke();
    }


    void Start()
    {
        base.Start();
    }


    void Update()
    {
        if (ShootButton())
            return;

        if (ReloadButton())
            return;

        text.text = currentAmmo + "/" + maxAmmo;
    }

    public void DualEffect()
    {
        secondEffect.Play();
        gunFire.Play();
    }
}
