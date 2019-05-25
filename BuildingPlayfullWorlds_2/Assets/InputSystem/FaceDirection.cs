using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceDirection : AbstractBehavior
{
	// Update is called once per frame
	void FixedUpdate()
    {
        var right = inputState.GetButtonValue(InputButtons[0]);
        var left = inputState.GetButtonValue(InputButtons[1]);

        if(right)
        {
            inputState.Direction = Directions.Right;
        }
        else if(left)
        {
            inputState.Direction = Directions.Left;
        }

        transform.localScale = new Vector3((float)inputState.Direction, 1, 1);
    }
}
