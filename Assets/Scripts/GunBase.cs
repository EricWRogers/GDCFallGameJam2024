using SolarStudios;
using UnityEngine;


namespace SolarStudios
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Animator))]

    public abstract class GunBase : MonoBehaviour
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
        private Animator anim;
      
        
        public abstract void OnShoot();
        public abstract void OnReload();
        public abstract void OnRayHit();

        [Header("Keybinds")]
        public KeyCode reloadKey;
        public KeyCode shootKey;
        [Space]
        [Header("Gun Specifics")]
        public int currentAmmo;
        public int maxAmmo;
        public float damage;
        public float fireRate;
        public float nextFireTime;
        public GameObject firePoint;
        [Tooltip("Only necessary for Raycast firing.")]
        public float raycastRange;
        public FireMode fireMode = FireMode.SingleFire;
        public FireType fireType = FireType.Prefab;
        [Header("Object pool. Use this instead of a bullet prefab")]
        public ObjectPool pool;
        [Tooltip("Bullet Prefab. Use this instead of objectpool")]
        public GameObject bulletPrefab;
        private bool doUsePool = false;


        private void Start()
        {
            anim = GetComponent<Animator>();
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
            if(pool != null)
            {
                doUsePool = true;
            }
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
                nextFireTime = Time.time + fireRate;
                OnShoot();
                gunFire.Play();
                currentAmmo--;
                pool.Spawn(firePoint.transform.position, Quaternion.identity);
            }

            if (fireMode == FireMode.RapidFire && Input.GetKey(shootKey) && Time.time >= nextFireTime && currentAmmo != 0)
            {
                nextFireTime = Time.time + fireRate;
                OnShoot();
                gunFire.Play();
                currentAmmo--;
                
            }
        }

        void ShootRaycast()
        {
            nextFireTime = Time.time + fireRate;

            RaycastHit hit;

            if (Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out hit, raycastRange))
            {
                if (hit.transform.CompareTag("Enemy"))
                {
                    nextFireTime = Time.time + fireRate;
                    OnRayHit();
                    gunFire.Play();
                    currentAmmo--;
                }
            }
        }

        void SpawnMethod(Transform trans, Quaternion rotation = default)
        {
            if (doUsePool)
            {
                pool.Spawn(trans.position, rotation);
            }
            else
            {
                Instantiate(bulletPrefab, trans.position, rotation);
            }
        }
    }
}