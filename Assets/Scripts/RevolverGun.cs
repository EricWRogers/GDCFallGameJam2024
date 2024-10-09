using SolarStudios;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class RevolverGun : GunBase
{

    public UnityEvent OnShootEvent;
    public UnityEvent OnReloadEvent;
    public TMP_Text text;
    public GameObject dual;


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
        OnShootEvent.Invoke();
    }

    
    void Start()
    {
        base.Start();
    }

    
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        if (ShootButton())
            return;
        
        if (ReloadButton())
            return;
        
        text. text = currentAmmo + "/" + maxAmmo;
    }

    private void OnDisable()
    {
        dual.SetActive(true);
    }
}
