using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class AgroEnemy : MonoBehaviour
{
    public float agroRange = 25.0f;
    public bool on = false;

    [SerializeField]
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            enemiesInZone.Add(child.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (on && transform.childCount == 0)
        {
            AgroEnemy[] argoZone = FindObjectsOfType<AgroEnemy>();

            int count = 0;

            foreach(AgroEnemy ae in argoZone)
            {
                if (ae.on == true)
                    count++;
            }

            if (count <= 1)
            {
                GameObject backgroundMusicObject = GameObject.Find("Background Music");
                GameObject battleMusicObject = GameObject.Find("Battle Music");

                // Check if both music objects exist before accessing them
                if (backgroundMusicObject != null && battleMusicObject != null)
                {
                    AudioSource backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
                    AudioSource battleMusic = battleMusicObject.GetComponent<AudioSource>();

                    if (backgroundMusic != null)
                    {
                        backgroundMusic.volume = 1.0f;
                    }
                    
                    if (battleMusic != null)
                    {
                        battleMusic.Stop();
                    }
                }              
            }

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && on == false)
        {
            on = true;
            GameObject backgroundMusicObject = GameObject.Find("Background Music");
            GameObject battleMusicObject = GameObject.Find("Battle Music");

            if (backgroundMusicObject != null && battleMusicObject != null)
            {
                AudioSource backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
                AudioSource battleMusic = battleMusicObject.GetComponent<AudioSource>();

                if (battleMusic != null && !battleMusic.isPlaying)
                {
                    if (backgroundMusic != null)
                        backgroundMusic.volume = 0.2f;

                    battleMusic.Play();
                }
            }

            foreach (GameObject enemy in enemiesInZone)
            {
                if (enemy == null) continue;  // Skip if the enemy has been destroyed

                if (enemy.GetComponent<ZombieStateMachine>() != null)
                {
                    var zombieStateMachine = enemy.GetComponent<ZombieStateMachine>();
                    zombieStateMachine.enabled = true;
                }
                else if (enemy.GetComponent<WerewolfStateMachine>() != null)
                {
                    var werewolfStateMachine = enemy.GetComponent<WerewolfStateMachine>();
                    werewolfStateMachine.enabled = true;
                }
                else if (enemy.GetComponent<VampireStateMachine>() != null)
                {
                    var vampireStateMachine = enemy.GetComponent<VampireStateMachine>();
                    vampireStateMachine.enabled = true;
                }
            }
        }
    }
}
