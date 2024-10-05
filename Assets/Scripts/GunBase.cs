using SolarStudios;
using Unity.Mathematics;
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
            Raycast,
            Frustum
        }


        public AudioSource gunFire;
        public AudioSource reload;
        [HideInInspector]
        public Animator anim;
      
        
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
        private float nextFireTime;
        public GameObject firePoint;
        [Tooltip("Only necessary for Raycast firing.")]
        public float raycastRange;
        public FireMode fireMode = FireMode.SingleFire;
        public FireType fireType = FireType.Prefab;
        [Header("Object pool. Use this instead of a bullet prefab")]
        public ObjectPool pool;
        [Tooltip("Bullet Prefab. Use this instead of objectpool")]
        public GameObject bulletPrefab;
        public bool doUsePool = false;

        [Header("Doom Specific Shooting")]
        public float shootRange = 100f;         
        public LayerMask enemyLayer; 
        public Camera playerCamera; 

        private void Start()
        {
            currentAmmo = maxAmmo;
            anim = GetComponent<Animator>();
            
            if(pool != null)
            {
                doUsePool = true;
            }
        }

        public void ShootButton()
        {
            if(Input.GetKeyDown(shootKey) && Time.time >= nextFireTime && currentAmmo != 0)
            {
            if (fireType == FireType.Prefab)
            {
                ShootPrefab();
            }
            else if (fireType == FireType.Raycast)
            {
                ShootRaycast();
            }
            else if (fireType == FireType.Frustum)
            {
                ShootFrustum();
            }
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
            if (fireMode == FireMode.SingleFire )
            {
                nextFireTime = Time.time + fireRate;
                OnShoot();
                gunFire.Play();
                currentAmmo--;
                SpawnMethod(firePoint.transform, Quaternion.identity);
            }

            if (fireMode == FireMode.RapidFire)
            {
                nextFireTime = Time.time + fireRate;
                OnShoot();
                gunFire.Play();
                currentAmmo--;
                SpawnMethod(firePoint.transform, Quaternion.identity);
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
        void ShootFrustum()
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCamera);

        
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject enemy in enemies)
            {
                if (GeometryUtility.TestPlanesAABB(planes, enemy.GetComponent<Collider>().bounds))
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance && distance <= shootRange)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }
        
            if (closestEnemy != null)
            {
                Debug.Log("Shooting " + closestEnemy.name);
            }
            else
            {
                Debug.Log("No enemy in range");
            }
        }
    }
}