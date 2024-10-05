using SolarStudios;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ObjectPool))]
public abstract class WeaponBase : MonoBehaviour
{
    public enum FireMode
    {
        SingleFire,
        RapidFire
    }

    public enum FireType
    {
        Prefab,
        Raycast
    }


    private AudioSource gunFire;
    private AudioSource reload;
    private ObjectPool pool;

    public KeyCode reloadKey;
    public KeyCode shootKey;
    public abstract void OnShoot();
    public abstract void OnReload();

    public int currentAmmo;
    public int maxAmmo;
    public float damage;
    public float nextFireTime;

    public FireMode fireMode = FireMode.SingleFire;
    public FireType fireType = FireType.Prefab;


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

        pool = GetComponent<ObjectPool>();
    }

    public void ShootButton()
    {
        if (fireType == FireType.Prefab)
        {
            ShootPrefab();
        }
        if (fireType == FireType.Raycast)
        {
            ShootRaycast();
        }
    }

    public void ReloadButton()
    {
        if (Input.GetKeyDown(reloadKey))
        {
            Reload();
        }
    }

    public void Reload()
    {
        OnReload();
        reload.Play();
        currentAmmo = maxAmmo;
    }

    public void ShootPrefab()
    {
        if (fireMode == FireMode.SingleFire && Input.GetKeyDown(shootKey) && Time.time >= nextFireTime && currentAmmo != 0 && fireMode == 0)
        {
            OnShoot();
            gunFire.Play();
            currentAmmo--;
            pool.Spawn(transform.position, Quaternion.identity);
        }

        if (fireMode == FireMode.RapidFire && Input.GetKey(shootKey) && Time.time >= nextFireTime && currentAmmo != 0)
        {
            OnShoot();
            gunFire.Play();
            currentAmmo--;
            pool.Spawn(transform.position, Quaternion.identity);
        }
    }

    public void ShootRaycast()
    {

    }
}
