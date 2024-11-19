using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;

public class VampireStateMachine : SimpleStateMachine
{
    public PhaseOneState phaseOne;
    public PhaseThreeState phaseTwo;
    public MoveAroundState move;

    [HideInInspector]
    public bool isAlive = false;
    public float attackRange = 1.0f;

    [HideInInspector]
    public AudioSource attackSource;

    private Health enemyHealth;
    [HideInInspector]
    public Transform target;
    private SimpleState currentState;

    void Awake()
    {
        states.Add(phaseOne);
        states.Add(phaseTwo);
        states.Add(move);

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
        
        float healthPercentage = GetHealthPercentage();
        move.UpdateCooldown(Time.deltaTime);

        // Check to enter Phase 2 if health is between 33% and 66%
        if (healthPercentage <= 50f)
        {
            ChangeState(nameof(PhaseThreeState));
        }

        if(healthPercentage <= 75f && move.CanEnterMoveState())
        {
            ChangeState(nameof(MoveAroundState));
        }


    }

    public float GetHealthPercentage()
    {
        float healthpercent = ((float)enemyHealth.currentHealth / (float)enemyHealth.maxHealth) * 100f;
        return healthpercent;
    }
}
