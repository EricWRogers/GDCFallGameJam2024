using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
/*
 * Plan for this Phase is to have targetting for the Guns and Move Towards the Player after spawning the Bats(Bullets)
 */
[System.Serializable]
public class PhaseTwoState : SimpleState
{
    private NavMeshAgent agent;
    private float attackRange;
    public float buffer = 5f;
    private bool isAttacking;
    public Timer timer;
    public UnityEvent spawnBats;
    public UnityEvent stopAttacking;

    public float lungeSpeed = 10.0f;
    public bool isLunging = false;
    public float lungeDuration = 2.0f;
    private float lungeTimeElapsed = 0f;

    public override void OnStart()
    {
        Debug.Log("Enter Phase One State");
        base.OnStart();

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            agent = vampireStateMachine.GetComponent<NavMeshAgent>();

            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(vampireStateMachine.transform.position);
            }
            attackRange = vampireStateMachine.attackRange + buffer;
        }

        timer.StartTimer(2, true);
        if (spawnBats == null)
        {
            spawnBats = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            if (vampireStateMachine.isAlive)
            {
                vampireStateMachine.transform.LookAt(vampireStateMachine.target);

                if (!isAttacking)
                {
                    isAttacking = true;
                    spawnBats.Invoke();
                }

                if (isLunging)
                {
                    LungeMove(dt);
                }

                if (timer.timeLeft <= 0 && !isLunging)
                {
                    LungeAttack();
                }

                timer.autoRestart = false;
                if (timer.timeLeft <= 0)
                {
                    isAttacking = false;
                    stateMachine.ChangeState(nameof(ChaseState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }



    public void LungeAttack()
    {
        Debug.Log("The Vampired Lunged");

        isLunging = true;
        lungeTimeElapsed = 0f; 
    }

    public void LungeMove(float dt)
    {
        if (stateMachine is VampireStateMachine vampire)
        {
            Vector3 lungeDirection = vampire.transform.forward;
            agent.Move(lungeDirection * lungeSpeed * dt);
            lungeTimeElapsed += dt;

            if (lungeTimeElapsed >= lungeDuration)
            {
                isLunging = false;
                stateMachine.ChangeState(nameof(ChaseState));
            }
        }
    }
}
