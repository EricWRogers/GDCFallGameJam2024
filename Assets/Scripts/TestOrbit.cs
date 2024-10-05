using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestOrbit : MonoBehaviour
{
    public float speed = 20.0f;
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0.0f, speed, 0.0f) * Time.fixedDeltaTime);
    }
}
