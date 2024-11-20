using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPickUp : MonoBehaviour
{
    public GameObject pickup;
    public float rotateSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickup.transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ScoreManager.instance.AddMultiplier(1);

            Destroy(gameObject);
        }
    }
}
