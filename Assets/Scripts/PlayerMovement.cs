using SuperPupSystems.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float gravity;
    public float speed;
    public float sprintSpeed;
    public float camSpeed;
    public float maxSpeed = 10f;

    private float currentRotationSpeed = 0f;  // Track the current rotation speed
    public float accelerationRate = 5f;       // Rate at which the rotation accelerates
    public float maxRotationSpeed = 100f;  

    private float rotationX = 0f;
    public bool dual;

    private Camera cam;
    private Vector3 move;
    [HideInInspector]
    public Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
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
        float targetRotationSpeed = Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;

        // Acceleration so it is not so fast
        currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, targetRotationSpeed, accelerationRate * Time.deltaTime);

        if(!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.Rotate(Vector3.up * currentRotationSpeed, Space.Self);
        }
    }
    

    private void Move()
    {

        float currentSpeed;

        float moveX;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            moveX = Input.GetAxisRaw("Horizontal");    
        }
        else
        {
            moveX = 0.0f;Input.GetAxisRaw("Horizontal");
        }
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        //Debug.Log(move);

        move.Normalize();

        rb.AddForce(move * currentSpeed * Time.deltaTime, ForceMode.Force);


        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        if(!IsGrounded())
        {
            rb.AddForce(Vector3.down * gravity * Time.deltaTime);
        }

        if (!dual)
        {
            if (rb.velocity.magnitude > 0.1 && !CheckAnim("GunShoot") && !CheckAnim("Reload"))
            {
                anim.Play("GunSway");
            }
        }
        else
        {
            if (rb.velocity.magnitude > 0.1 && !CheckAnim("DualGunShoot") && !CheckAnim("DualReload"))
            {
                Debug.Log("Work");
                anim.Play("DualGunSway");
            }
        }
    }

    public bool CheckAnim(string name)
    {
        if (anim == null)
        {
            anim = gameObject.GetComponentInChildren<Animator>();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
