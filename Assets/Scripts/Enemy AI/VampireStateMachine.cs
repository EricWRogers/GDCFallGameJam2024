using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;

public class VampireStateMachine : SimpleStateMachine
{
    public ChaseState chase;
    public PhaseOneState phaseOne;
    public PhaseTwoState phaseTwo;

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
        states.Add(phaseOne);
        states.Add(phaseTwo);

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
        else if (enemyHealth.currentHealth <= 0)
        {
            isAlive = false;
        }
        if(GetHealthPercentage() < 95f && GetHealthPercentage() > 60f)
        {
            //Phase 1 (Summoner Phase No Tracking)
        }
        if (GetHealthPercentage() <= 60f && GetHealthPercentage() > 0f)
        {
            //Enter Phase 2 of Boss Fight (Summoner/Melee with Tracking)
        }
        
    }

    public float GetHealthPercentage()
    {
        return (enemyHealth.currentHealth / enemyHealth.maxHealth) * 100f;
    }
}
