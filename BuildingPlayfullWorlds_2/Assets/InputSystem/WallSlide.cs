using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlide : StickToWall
{
    public float SlideVelocity = -5f;

    override protected void Update()
    {
        base.Update();

        if (OnWallDetected)
        {
            var velY = SlideVelocity;

            body2d.velocity = new Vector2(body2d.velocity.x, velY);
        }
    }

    override protected void OnStick()
    {
        body2d.velocity = Vector2.zero;
    }

    override protected void OffWall()
    {
        //do nothing
    }
}
