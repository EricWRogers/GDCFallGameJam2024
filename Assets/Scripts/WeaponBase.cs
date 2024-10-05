using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    
    private AudioSource gunFire;
    private AudioSource reload;

    private void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>(); //Not a mega fan of this but ig it works.
        if (audioSources.Length >= 2)
        {
            gunFire = audioSources[0]; 
            reload = audioSources[1];   
        }
        else
        {
            Debug.LogError("Not enough AudioSource components found!");
        }
    }

    public void OnShoot()
    {

    }
}
