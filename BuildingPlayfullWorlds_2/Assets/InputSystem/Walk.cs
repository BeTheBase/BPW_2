using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : AbstractBehavior
{
    public float Speed = 50f;
    public float RunMultiplier = 2f;
    public bool Running;

    void Update ()
    {
        Running = false;

        var right = inputState.GetButtonValue(InputButtons[0]);
        var left = inputState.GetButtonValue(InputButtons[1]);
        var run = inputState.GetButtonValue(InputButtons[2]);

        if (right || left)
        {
            var tmpSpeed = Speed;

            if(run && RunMultiplier > 0)
            {
                tmpSpeed *= RunMultiplier;
                Running = true;
            }

            var velX = tmpSpeed * (float)inputState.Direction;

            body2d.velocity = new Vector2(velX, body2d.velocity.y);
        }



    }
}
