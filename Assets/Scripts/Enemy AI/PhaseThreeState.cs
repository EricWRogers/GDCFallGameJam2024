using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using UnityEngine.Events;
/*
 * Plan for this Phase is to have targetting for the Bats(Bullets) and increase the attack rate
 */
[System.Serializable]
public class PhaseThreeState : SimpleState
{
    private float attackRange;
    public float buffer = 25f;
    private bool isAttacking;
    public Timer timer;
    public UnityEvent spawnBats;

    public UnityEvent stopAttacking;

    public override void OnStart()
    {
        Debug.Log("Enter Phase One State");
        base.OnStart();

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
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
}
