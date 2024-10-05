using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;

public class AgroEnemy : MonoBehaviour
{
    public float agroRange = 25.0f;
    [SerializeField]
    private List<GameObject> enemiesInZone = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            enemiesInZone.Add(child.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            Destroy(this, 2);
        }
    }
}
