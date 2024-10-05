using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float sprintSpeed;
    public float camSpeed;
    public float maxSpeed = 10f;

    private float rotationX = 0f;


    private Camera cam;
    private Vector3 move;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        Move();
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * camSpeed;
       
        transform.Rotate(Vector3.up * mouseX);
        
        //cam.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
    

    private void Move()
    {
        float currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();

        rb.AddForce(move * currentSpeed, ForceMode.Force);


        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
