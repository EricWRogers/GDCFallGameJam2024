using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
/*
 * Plan for this Phase is to have targetting for the Bats(Bullets)
 */
[System.Serializable]
public class PhaseTwoState : SimpleState
{
    private float attackRange;
    public float buffer = 25f;
    private bool isAttacking;
    public Timer phaseTwoTimer;
    public UnityEvent spawnBats;
    public UnityEvent stopAttacking;

    private float tim = 0.0f;

    public override void OnStart()
    {
        Debug.Log("Enter Phase Two State");
        base.OnStart();

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            attackRange = vampireStateMachine.attackRange + buffer;
        }

        Debug.Log("STARTTIMER");
        tim = 0.0f;

        isAttacking = true;
    }

    public override void UpdateState(float dt)
    {
        tim -= dt;

        if (stateMachine is VampireStateMachine vampireStateMachine && vampireStateMachine.isAlive)
        {
            vampireStateMachine.transform.LookAt(vampireStateMachine.target);

            // Check if isAttacking is true and timer has reached zero
            if (isAttacking && tim <= 0.0f)
            {
                Debug.Log("Spawn the bats");
                spawnBats.Invoke();
                tim = 2.0f;

                isAttacking = false; // Stop attacking after first trigger
                //stopAttacking.Invoke();
            }

            if (vampireStateMachine.GetHealthPercentage() <= 33f)
            {
                vampireStateMachine.ChangeState(nameof(PhaseThreeState));
            }
        }
    }

    public override void OnExit()
    {
        // Remove the listener to prevent duplicate calls when re-entering this state
        if (spawnBats != null)
        {
            Debug.Log("EA Remove Listener");
            phaseTwoTimer.timeout.RemoveListener(SpawnBats);
        }
        stopAttacking.Invoke();
        base.OnExit();
    }

    public void SpawnBats()
    {
        Debug.Log("EA SpawnBats method called");
        spawnBats?.Invoke();
    }
}
