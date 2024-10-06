using SuperPupSystems.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ChaseState : SimpleState
{

    private NavMeshAgent agent;
    private float attackRange;
    public float buffer = 5f;
    public override void OnStart()
    {
        Debug.Log("Enter Chase State");
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
            attackRange = werewolfStateMachine.attackRange + buffer;
        }

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            agent = vampireStateMachine.GetComponent<NavMeshAgent>();

            // Check if agent is active and on the NavMesh before setting destination
            if (agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(vampireStateMachine.transform.position);
            }
            attackRange = vampireStateMachine.attackRange + buffer;
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is ZombieStateMachine zombieStateMachine)
        {
            if (zombieStateMachine.isAlive)
            {
                // Check if agent is active and on the NavMesh before setting destination
                if (agent.isActiveAndEnabled && agent.isOnNavMesh)
                {
                    agent.SetDestination(zombieStateMachine.target.position);
                }
                else
                {
                    Debug.LogWarning("Zombie NavMeshAgent is not on NavMesh or not active.");
                }

                if (Vector3.Distance(agent.transform.position, zombieStateMachine.target.position) < attackRange)
                {
                    stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }

        if (stateMachine is WerewolfStateMachine werewolfStateMachine)
        {
            if (werewolfStateMachine.isAlive)
            {
                // Check if agent is active and on the NavMesh before setting destination
                if (agent.isActiveAndEnabled && agent.isOnNavMesh)
                {
                    agent.SetDestination(werewolfStateMachine.target.position);
                }
                else
                {
                    Debug.LogWarning("Werewolf NavMeshAgent is not on NavMesh or not active.");
                }

                if (Vector3.Distance(agent.transform.position, werewolfStateMachine.target.position) <= attackRange)
                {
                    stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            if (vampireStateMachine.isAlive)
            {
                // Check if agent is active and on the NavMesh before setting destination
                if (agent.isActiveAndEnabled && agent.isOnNavMesh)
                {
                    agent.SetDestination(vampireStateMachine.target.position);
                }
                else
                {
                    Debug.LogWarning("Werewolf NavMeshAgent is not on NavMesh or not active.");
                }

                if (Vector3.Distance(agent.transform.position, vampireStateMachine.target.position) <= attackRange)
                {
                    stateMachine.ChangeState(nameof(PhaseOneState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
