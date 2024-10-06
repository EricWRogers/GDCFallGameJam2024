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

    private Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        Move();
        Look();
    }

    void LateUpdate()
    {
        Camera.main.transform.forward = transform.forward;
        Camera.main.transform.position = transform.position;
        Camera.main.transform.position += new Vector3(0.0f, 0.62f, 0.0f);
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;

        Debug.Log(mouseX);
       
        transform.Rotate(Vector3.up * mouseX, Space.Self);
        
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
        float moveX = 0.0f;Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        //Debug.Log(move);

        move.Normalize();

        rb.AddForce(move * currentSpeed * Time.deltaTime, ForceMode.Force);


        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        if (rb.velocity.magnitude > 0.1 && !CheckAnim("GunShoot") && !CheckAnim("Reload"))
        {
            anim.Play("GunSway");
        }
    }

    public bool CheckAnim(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
