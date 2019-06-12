using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class ThirdPersonPlayerController : MonoBehaviour
{
    //Public's
    public float WalkSpeed = 6;
    public float RunSpeed = 10;
    public float StrafeSpeed = 5;
    public float Gravity = 20;
    public float JumpHeight = 2;
    public float ShootSpeed;
    public GameObject UI;


    public bool CanJump = true;
    public bool CanShoot = true;
    public bool IsRunning
    {
        get { return isRunning; }
    }

    //Private's
    private bool isRunning = false;
    private bool isGrounded = false;
    private Rigidbody RBody;

    private Vector3 StartPosition;

    void Awake()
    {
        if (!RBody)
            RBody.GetComponent<Rigidbody>();

        RBody.freezeRotation = true;
        RBody.useGravity = false;

        UI.SetActive(false);
    }

    void Start()
    {
        StartPosition = this.transform.position;
    }

    void FixedUpdate()
    {
        CheckGrounded();
        // get correct Speed
        float forwardAndBackSpeed = WalkSpeed;

        // if running, set run Speed
        if (isRunning)
        {
            forwardAndBackSpeed = RunSpeed;
        }

        // calculate how fast it should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal") * StrafeSpeed, 0, Input.GetAxis("Vertical") * forwardAndBackSpeed);
        targetVelocity = transform.TransformDirection(targetVelocity);

        // apply a force that attempts to reach our target velocity
        Vector3 velocity = RBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.y = 0;
        RBody.AddForce(velocityChange, ForceMode.VelocityChange);

        // jump
        if (CanJump && isGrounded && Input.GetButton("Jump"))
        {
            RBody.velocity = new Vector3(velocity.x, Mathf.Sqrt(2 * JumpHeight * Gravity), velocity.z);
            isGrounded = false;
        }

        // apply Gravity
        RBody.AddForce(new Vector3(0, -Gravity * RBody.mass, 0));


    }

    void Update()
    {
        // check if the player is touching a surface below them
        //CheckGrounded();

        // check if the player is running
        if (isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        // check if the player stops running
        if (!isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UI.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                UI.SetActive(false);
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                UI.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.layer == 0)
        //isGrounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        //if (other.gameObject.layer == 0)
        //isGrounded = false;
    }

    void CheckGrounded()
    {
        /* ==============
         * REMEMBER
         * ==============
         * If you change the size of the prefab, you may have
         * to change the length of the ray to ensure it hits
         * the ground.
         * 
         * All obstacles/walls/floors must have rigidbodies
         * attached to them. If not, Unity physics may get
         * confused and the player can jump really high
         * when in a corner between 2 walls for example.
         */
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        //Debug.DrawRay(ray.origin, ray.direction * rayLength);
        // if there is something directly below the player
        if (Physics.Raycast(ray, out hit, 2.1f))
        {
            isGrounded = true;
        }
    }
}