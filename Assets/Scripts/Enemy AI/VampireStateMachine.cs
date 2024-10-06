using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;

public class VampireStateMachine : SimpleStateMachine
{
    public ChaseState chase;
    public AttackState attack;

    [HideInInspector]
    public bool isAlive = false;
    public float attackRange = 1.0f;

    [HideInInspector]
    public AudioSource attackSource;

    private Health enemyHealth;
    [HideInInspector]
    public Transform target;

    void Awake()
    {
        states.Add(chase);
        states.Add(attack);

        foreach (SimpleState s in states)
        {
            s.stateMachine = this;
        }
    }

    void Start()
    {
        enemyHealth = GetComponent<Health>();
        attackSource = GetComponent<AudioSource>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        ChangeState(nameof(ChaseState));
    }

    void Update()
    {
        if (enemyHealth.currentHealth > 0)
        {
            isAlive = true;
        }
        else
        {
            isAlive = false;
        }
    }
}
