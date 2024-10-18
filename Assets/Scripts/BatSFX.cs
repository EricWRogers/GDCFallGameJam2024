using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSFX : MonoBehaviour
{
    public AudioSource batSqueak;
    public AudioSource wingFlap;
    
    // Start is called before the first frame update
    void Start()
    {
        batSqueak.Play();
        wingFlap.Play();
    }
}
