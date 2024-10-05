using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AttackState : SimpleState
{
    public Timer time;
    public UnityEvent attack;
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private float attackRange = 0f;
    private bool playerInRange;
    public bool isAttacking;
    public override void OnStart()
    {
        base.OnStart();

        if (stateMachine is ZombieStateMachine zombieStateMachine)
        {
            agent = zombieStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(zombieStateMachine.transform.position);
            attackRange = zombieStateMachine.attackRange + 0.5f;
        }

        if (stateMachine is WerewolfStateMachine werewolfStateMachine)
        {
            agent = werewolfStateMachine.GetComponent<NavMeshAgent>();
            agent.SetDestination(werewolfStateMachine.transform.position);
            attackRange = werewolfStateMachine.attackRange + 0.5f;
        }

        time.StartTimer(2, true);
        if (attack == null)
        {
            attack = new UnityEvent();
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is ZombieStateMachine zombieStateMachine)
        {
            zombieStateMachine.transform.LookAt(zombieStateMachine.target);

            if (!isAttacking)
            {
                isAttacking = true;
                attack.Invoke();
            }

            if (Vector3.Distance(agent.transform.position, zombieStateMachine.target.position) > zombieStateMachine.attackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChaseState));
                }
            }
        }

        if (stateMachine is WerewolfStateMachine werewolfStateMachine)
        {
            werewolfStateMachine.transform.LookAt(werewolfStateMachine.target);

            if (!isAttacking)
            {
                isAttacking = true;
                attack.Invoke();
            }

            if (Vector3.Distance(agent.transform.position, werewolfStateMachine.target.position) > werewolfStateMachine.attackRange)
            {
                time.autoRestart = false;
                if (time.timeLeft <= 0)
                {
                    isAttacking = false;
                    stopAttacking.Invoke();
                    stateMachine.ChangeState(nameof(ChaseState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public void WerewolfLungeAttack()
    {
        //if (stateMachine is WerewolfStateMachine werewolfStateMachine)
        //{
        //    agent = werewolfStateMachine.GetComponent<NavMeshAgent>();
        //    agent.SetDestination(werewolfStateMachine.transform.position);
        //    attackRange = werewolfStateMachine.attackRange + 5f;
        //}
    }
}
