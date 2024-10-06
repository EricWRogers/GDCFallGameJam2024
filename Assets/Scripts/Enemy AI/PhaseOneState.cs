using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SuperPupSystems.StateMachine;
using SuperPupSystems.Helper;
using UnityEngine.Events;

[System.Serializable]
public class PhaseOneState : SimpleState
{
    private float attackRange;
    public float buffer = 25f;
    private bool isAttacking;
    public Timer phaseOneTimer;
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

        // Start the timer and enable autoRestart
        phaseOneTimer.StartTimer(2, true);
        phaseOneTimer.autoRestart = true;

        isAttacking = true;

        // Add listener for spawning bats once
        if (spawnBats != null)
        {
            phaseOneTimer.timeout.AddListener(SpawnBats);
        }
    }

    public override void UpdateState(float dt)
    {
        if (stateMachine is VampireStateMachine vampireStateMachine && vampireStateMachine.isAlive)
        {
            vampireStateMachine.transform.LookAt(vampireStateMachine.target);

            // Check if isAttacking is true and timer has reached zero
            if (isAttacking && phaseOneTimer.timeLeft <= 0)
            {
                Debug.Log("Spawn the bats");
                spawnBats.Invoke();

                isAttacking = false; // Stop attacking after first trigger
                stopAttacking.Invoke();
            }

            if (vampireStateMachine.GetHealthPercentage() <= 50 && !(stateMachine.stateName.ToLower() == typeof(PhaseTwoState).ToString().ToLower() ))
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
            phaseOneTimer.timeout.RemoveListener(SpawnBats);
        }
        stopAttacking.Invoke();
        base.OnExit();
    }

    public void SpawnBats()
    {
        spawnBats.Invoke();
    }
}
