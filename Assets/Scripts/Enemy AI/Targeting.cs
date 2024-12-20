using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    private GameObject target;

    public float trackingRange = 5f;
    void Start()
    {
        // If TargetingSystem is found call FindTarget
        target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();

        if (target)
            gameObject.transform.LookAt(target.transform);
    }

    void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > trackingRange)
        {
            if (target)
            {
                gameObject.transform.LookAt(target.transform);
            }
            else
            {
                target = gameObject.GetComponent<TargetingSystem>()?.FindTarget();
                if (target)
                {
                    gameObject.transform.LookAt(target.transform);
                }
            }
        }
    }
}