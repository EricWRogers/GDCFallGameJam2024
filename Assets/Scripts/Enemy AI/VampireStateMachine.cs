using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;

public class VampireStateMachine : SimpleStateMachine
{
    public PhaseOneState phaseOne;
    //public PhaseTwoState phaseTwo;
    public PhaseThreeState phaseThree;

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
        //states.Add(phaseTwo);
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
        
        float healthPercentage = GetHealthPercentage();

        // Check to enter Phase 2 if health is between 33% and 66%
        if (healthPercentage <= 50f)
        {
            ChangeState(nameof(PhaseThreeState));
        }


    }

    public float GetHealthPercentage()
    {
        Debug.Log("Current Health: " + enemyHealth.currentHealth);
        Debug.Log("Max Health: " + enemyHealth.maxHealth);
        float healthpercent = ((float)enemyHealth.currentHealth / (float)enemyHealth.maxHealth) * 100f;
        Debug.Log("Health percent: " + healthpercent);
        return healthpercent;
    }
}
