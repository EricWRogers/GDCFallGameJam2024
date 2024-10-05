using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[System.Serializable]
public class AttackState : SimpleState
{
    public Timer time;
    public Timer werewolfTimer;
    public UnityEvent attack;
    public UnityEvent stopAttacking;
    private NavMeshAgent agent;
    private float attackRange = 0f;
    private bool playerInRange;
    public bool isAttacking;

    public float lungeSpeed = 10.0f;
    public bool isLunging = false;
    public float lungeDuration = 2.0f;
    private float lungeTimeElapsed = 0f;

    public override void OnStart()
    {
        Debug.Log("Enter Attacking State");
        base.OnStart();

        if (stateMachine is ZombieStateMachine zombieStateMachine)
        {
            agent = zombieStateMachine.GetComponent<NavMeshAgent>();

            // Check if agent is active and on the NavMesh before setting destination
            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(zombieStateMachine.transform.position);
            }
            attackRange = zombieStateMachine.attackRange + 0.5f;
        }

        if (stateMachine is WerewolfStateMachine werewolfStateMachine)
        {
            agent = werewolfStateMachine.GetComponent<NavMeshAgent>();

            // Check if agent is active and on the NavMesh before setting destination
            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(werewolfStateMachine.transform.position);
            }
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
                attack.Invoke(); // Trigger attack event
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
                attack.Invoke(); // Trigger attack event
            }

            if (isLunging)
            {
                LungeMove(dt);
            }

            if (time.timeLeft <= 0 && !isLunging)
            {
                WerewolfLungeAttack();
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
        //agent.enabled = false;
        Debug.Log("The Werewolf Lunged");

        isLunging = true; // Mark lunge as in progress
        lungeTimeElapsed = 0f; // Reset the timer
    }

    public void LungeMove(float dt)
    {
        if (agent.isOnNavMesh)
        {
            Vector3 lungeDirection = ((WerewolfStateMachine)stateMachine).transform.forward;

            // Move manually during lunge
            agent.Move(lungeDirection * lungeSpeed * dt);
            //((WerewolfStateMachine)stateMachine).transform.Translate(lungeDirection * lungeSpeed * dt, Space.World);

            lungeTimeElapsed += dt;

            if (lungeTimeElapsed >= lungeDuration || Vector3.Distance(((WerewolfStateMachine)stateMachine).transform.position, ((WerewolfStateMachine)stateMachine).target.position) < attackRange)
            {
                isLunging = false;
                stopAttacking.Invoke();
                stateMachine.ChangeState(nameof(ChaseState));
            }
        }
        else
        {
            Debug.LogWarning("The AI is not on the NavMesh!");
            isLunging = false;
            stopAttacking.Invoke();
            stateMachine.ChangeState(nameof(ChaseState));
        }
    }
}
