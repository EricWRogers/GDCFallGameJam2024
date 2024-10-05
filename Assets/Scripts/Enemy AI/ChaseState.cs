using SuperPupSystems.StateMachine;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ChaseState : SimpleState
{

    private NavMeshAgent agent;
    private float attackRange;
    public override void OnStart()
    {
        Debug.Log("Enter Chase State");
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
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is ZombieStateMachine zombieStateMachine)
        {
            if (zombieStateMachine.isAlive)
            {
                agent.SetDestination(zombieStateMachine.target.position);

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
                agent.SetDestination(werewolfStateMachine.target.position);

                if (Vector3.Distance(agent.transform.position, werewolfStateMachine.target.position) < attackRange)
                {
                    stateMachine.ChangeState(nameof(AttackState));
                }
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
