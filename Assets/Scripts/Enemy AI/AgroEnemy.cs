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
                AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
                AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

                backgroundMusic.volume = 1.0f;
                battleMusic.Stop();
            }

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && on == false)
        {
            on = true;
            AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            if (battleMusic.isPlaying == false)
            {
                backgroundMusic.volume = 0.2f;
                battleMusic.Play();
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
