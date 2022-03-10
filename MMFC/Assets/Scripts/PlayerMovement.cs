using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Thank you to plai on youtube for the series that halped me develop this!! - Aneesh
public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;
    float sprint = 1.0f;
    bool coolDown = false;
    public float dodgeTime;

    [SerializeField] Transform orientation;
    [SerializeField] Transform cameraPosition;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float movementMultiplier = 10f;
    [SerializeField] float airMultiplier = .4f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Drag")]
    float groundDrag = 6f;
    float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    float groundDistance = 0.4f;
    bool crouch;

    float dodgeSpeed;
    

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;
    Vector3 placeholder;

    Rigidbody rb;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + .5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Start()
    {
        dodgeSpeed = 15.0f;
        crouch = false;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);
        ControlDrag();
        MyInput();


        if (Input.GetKeyDown(jumpKey) && isGrounded && !crouch)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            Crouch();
        }


        if (Input.GetKeyDown("left shift") && !(Input.GetKey("w")) && !coolDown)
            Dodge();

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void Dodge()
    {
        if ((Input.GetKey("a")))
        {
            print("Dodging Left " + dodgeSpeed);
            rb.AddForce(-transform.right * dodgeSpeed, ForceMode.Impulse);

            coolDown = true;
            Invoke("ResetCoolDown", dodgeTime);
        }
        else if ((Input.GetKey("d")))
        {
            print("Dodging Right " + dodgeSpeed);
            rb.AddForce(transform.right * dodgeSpeed, ForceMode.Impulse);

            coolDown = true;
            Invoke("ResetCoolDown", dodgeTime);
        }
        else if ((Input.GetKey("s") && !crouch))
        {
            print("Dodging Back " + dodgeSpeed);
            rb.AddForce(-transform.forward * dodgeSpeed, ForceMode.Impulse);

            coolDown = true;
            Invoke("ResetCoolDown", dodgeTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    void Crouch()
    {
        if (!crouch)
        {
            //Rotate to prone
            placeholder = transform.eulerAngles;
            placeholder.x = 90.0f;
            transform.eulerAngles = placeholder;

            dodgeSpeed = 7.0f;

            crouch = true;
        }
        else if (placeholder.x > 0.0f)
        {
            //Rotate to stand
            placeholder = transform.eulerAngles;
            placeholder.x = placeholder.x - 0.1f;
            transform.eulerAngles = placeholder;

            dodgeSpeed = 15.0f;

            crouch = false;
        }
        //else
        //{
        //    //Rotate to stand
        //    placeholder = transform.eulerAngles;
        //    placeholder.x = 0.0f;
        //    transform.eulerAngles = placeholder;

        //    dodgeSpeed = 15.0f;

        //    crouch = false;
        //}
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void ResetCoolDown()
    {
        coolDown = false;
    }

    void MovePlayer()
    {   
        if (Input.GetKey("left shift") && Input.GetKey("w") && !(Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && !crouch)
        {
            print("Sprinting");
            sprint = 1.5f;
        }
        else
            sprint = 1.0f;

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * (sprint * moveSpeed) * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }

    }
}
