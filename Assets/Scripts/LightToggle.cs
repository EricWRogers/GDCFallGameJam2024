using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToggle : MonoBehaviour
{
    public float radius;
    private Transform m_player;
    private Light m_light;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_light.enabled = (Vector3.Distance(transform.parent.position, m_player.position) < radius);
    }
}
