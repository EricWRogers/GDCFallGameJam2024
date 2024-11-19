using SuperPupSystems.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[System.Serializable]
public class MoveAroundState : SimpleState
{
    public Transform spotOne;
    public Transform spotTwo;
    public Transform spotThree;
    public float healthPercent;
    public float waitDuration = 2.0f;  // Duration of state
    private float waitTimer;
    public float waitCooldown = 5.0f;  // Time before AI can move again
    private float cooldownTimer;
    
    // Start is called before the first frame update
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log("Entering Move State");

        if (stateMachine is VampireStateMachine vampireStateMachine)
        {
            healthPercent = vampireStateMachine.GetHealthPercentage();
            Transform newSpot = GetRandomSpotExcludingCurrent(vampireStateMachine.transform.position);
            vampireStateMachine.transform.position = newSpot.position;
        }

        waitTimer = 1.0f;
    }

    private Transform GetRandomSpotExcludingCurrent(Vector3 currentPosition)
    {
        List<Transform> possibleSpots = new List<Transform> { spotOne, spotTwo, spotThree };

        // Remove the current position from the list
        possibleSpots.RemoveAll(spot => Vector3.Distance(spot.position, currentPosition) < 1.0f);

        // Select a random spot from the remaining options
        return possibleSpots[Random.Range(0, possibleSpots.Count)];
    }

    // Update is called once per frame
    public override void UpdateState(float dt)
    {
        //Keeps AI in this State
        if (waitTimer > 0)
        {
            waitTimer -= dt;
        }
        if (waitTimer <= 0 && healthPercent >= 51.0f)
        {
            stateMachine.ChangeState(nameof(PhaseOneState));
        }
        else if(waitTimer <= 0 && healthPercent <= 50.0f)
        {
            stateMachine.ChangeState(nameof(PhaseThreeState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        cooldownTimer = waitCooldown;
    }

    public bool CanEnterMoveState()
    {
        return cooldownTimer <= 0;  
    }

    public void UpdateCooldown(float _dt)
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= _dt;
        }
    }
}
