using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : AbstractBehavior
{
    public Vector2 JumpVelocity = new Vector2(50, 200);
    public bool JumpingOffWall;
    public float ResetDelay = .2f;

    private float timeElapsed = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (collisionState.OnWall && !collisionState.Standing)
        {
            var canJump = inputState.GetButtonValue(InputButtons[0]);

            if (canJump && !JumpingOffWall)
            {
                inputState.Direction = inputState.Direction == Directions.Right ? Directions.Left : Directions.Right;
                body2d.velocity = new Vector2(JumpVelocity.x * (float)inputState.Direction, JumpVelocity.y);

                ToggleScripts(false);
                JumpingOffWall = true;
            }
        }

        if (JumpingOffWall)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > ResetDelay)
            {
                ToggleScripts(true);
                JumpingOffWall = false;
                timeElapsed = 0;
            }
        }
    }
}
