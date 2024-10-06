using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;

public class VampireStateMachine : SimpleStateMachine
{
    public PhaseOneState phaseOne;
    public PhaseTwoState phaseTwo;
    public PhaseThreeState phaseThree;

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
        states.Add(phaseOne);
        states.Add(phaseTwo);
        states.Add(phaseThree);

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

        ChangeState(nameof(PhaseOneState));
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
        if (GetHealthPercentage() <= 66f && GetHealthPercentage() > 33f)
        {
            //Enter Phase 2 of Boss Fight (Summoner/Melee with Tracking)
            ChangeState(nameof(PhaseTwoState));
        }
        if (GetHealthPercentage() <= 66f && GetHealthPercentage() > 33f)
        {
            //Enter Phase 3 of Boss Fight (More Bats)
            ChangeState(nameof(PhaseThreeState));
        }


    }

    public float GetHealthPercentage()
    {
        return (enemyHealth.currentHealth / enemyHealth.maxHealth) * 100f;
    }
}
