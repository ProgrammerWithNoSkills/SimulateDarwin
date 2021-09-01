using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 12.0f;
    public float sprint = 18.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    public float staminaLossPerFrame = 0.025f;
    public Stamina stamina;
    public Rigidbody rb;
    private Vector3 moveDirection;
    private CharacterController controller;
    private float hasmoved = 3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (stamina.curStamina > 0))
            {
                stamina.ReducePlayerStamina(0.025f);
            }
        //rb.velocity.magnitude = speed
        if (rb.velocity.magnitude > 17.1)
        {
            hasmoved = 3f;
        }
        if (rb.velocity.magnitude <= 17.1)
        {
            hasmoved -= Time.deltaTime;
        }
        if (rb.velocity.magnitude <= 18 && rb.velocity.magnitude > 8 && stamina.curStamina < 100 && hasmoved < 0)
        {
            stamina.ReducePlayerStamina(-0.070f);
        }
        if (rb.velocity.magnitude < 8 && stamina.curStamina < 100 && hasmoved < 0)
        {
            stamina.ReducePlayerStamina(-0.25f);
        }
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            //get key input and parse into Vector3
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            //parse local Vector3 to global space using TransformDirection()
            moveDirection = transform.TransformDirection(moveDirection);

            //if sprint key is pressed, and stamina is not 0
            if (Input.GetKey(KeyCode.LeftShift) && (stamina.curStamina > 0))
            {
                moveDirection *= sprint;

                //call reducePlayerStamina function
                stamina.ReducePlayerStamina(staminaLossPerFrame);
            }
            else
            {
                moveDirection *= speed;
            }

            //if jump key pressed, set vertical movement to jumpSpeed.
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        moveDirection.y -= gravity * Time.deltaTime;

        //move controller using global moveDirection
        controller.Move(moveDirection * Time.deltaTime);
    }
}
