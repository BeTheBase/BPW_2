using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : AbstractBehavior
{
    public GameObject JumpEffect;
    public float JumpSpeed = 200f;
    public float JumpDelay = .1f;
    public int JumpCount = 2;

    protected float lastJumpTime = 0;
    protected int jumpsRemaining = 0;
    private int tmpJumpCount;

    public override void Start()
    {
        base.Awake();

        tmpJumpCount = JumpCount;

        if (JumpEffect == null)
            JumpEffect = transform.GetChild(3).gameObject;

    }

    void FixedUpdate ()
    {
        var canJump = inputState.GetButtonValue(InputButtons[0]);
        var holdTime = inputState.GetButtonHoldTime(InputButtons[0]);

        if(collisionState.Standing)
        {
            JumpEffect.SetActive(false);

            if (colorManager.playerColors != ColorS.White)
                JumpCount = 1;
            else
                JumpCount = tmpJumpCount;

            if (canJump && holdTime < .1f)
            {
                //AManager.PlayPlayerSound("PlayerJump");
                jumpsRemaining = JumpCount - 1;
                OnJump();
            }
        }
        else
        {
            if (canJump && holdTime < .1f && Time.time - lastJumpTime > JumpDelay)
            {
                if (jumpsRemaining > 0)
                {
                    OnJump();
                    jumpsRemaining--;
                }
            }
        }
	}

    public virtual void OnJump()
    {
        if (colorManager.playerColors == ColorS.White)
            JumpEffect.SetActive(true);
        else
            JumpEffect.SetActive(false);

        var vel = body2d.velocity;
        lastJumpTime = Time.time;
        body2d.velocity = new Vector2(vel.x, JumpSpeed);

    }
}
