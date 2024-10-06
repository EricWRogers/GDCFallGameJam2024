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
            AudioSource backgroundMusic = GameObject.Find("Background Music").GetComponent<AudioSource>();
            AudioSource battleMusic = GameObject.Find("Battle Music").GetComponent<AudioSource>();

            backgroundMusic.volume = 1.0f;
            battleMusic.Stop();

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

            backgroundMusic.volume = 0.2f;
            battleMusic.Play();

            foreach (GameObject enemy in enemiesInZone)
            {
                var zombieStateMachine = enemy.GetComponent<ZombieStateMachine>();
                var werewolfStateMachine = enemy.GetComponent<WerewolfStateMachine>();

                if (zombieStateMachine != null)
                {
                    zombieStateMachine.enabled = true;
                }
                else if (werewolfStateMachine != null)
                {
                    werewolfStateMachine.enabled = true;
                }
            }
        }
    }
}
