using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerType
    {
        Player1,
        Player2
    }

    public PlayerType ThisPlayer;

    public float MovementSpeed = 5f;
    public float AttackSpeed = 1f;
    public float AttackDamage = 3f;
    public float JumpForce = 10f;
    public int MaxJumps = 2;

    private bool grounded;
    private int jumps;
    private Rigidbody rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (ThisPlayer == PlayerType.Player1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                Walk(1);
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                Walk(-1);
            }
            float horizontal = Input.GetAxis("Horizontal");

            Vector3 velocity = (transform.right * horizontal) * MovementSpeed * Time.deltaTime;
            velocity.y = rBody.velocity.y;
            rBody.velocity = velocity;
        }
        else if(ThisPlayer == PlayerType.Player2)
        {
            if(Input.GetKeyDown(KeyCode.RightShift))
            {
                Jump();
            }
        }
    }

    private void Walk(int side)
    {
        //transform.position += new Vector3(side * MovementSpeed * Time.deltaTime, transform.position.y ,transform.position.z);
    }

    private void Jump()
    {
        if (jumps > 0)
        {
            rBody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            grounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumps = MaxJumps;
            grounded = true;
        }
    }
}
