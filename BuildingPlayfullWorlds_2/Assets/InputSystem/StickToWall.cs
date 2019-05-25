using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToWall : AbstractBehavior
{

    public bool OnWallDetected;

    protected float defaultGravityScale;

    protected float defaultDrag;

    // Use this for initialization
    void Start()
    {
        defaultGravityScale = body2d.gravityScale;
        defaultDrag = body2d.drag;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (collisionState.OnWall)
        {
            if (!OnWallDetected)
            {
                OnStick();
                ToggleScripts(false);
                OnWallDetected = true;
            }
        }
        else
        {
            if (OnWallDetected)
            {
                OffWall();
                ToggleScripts(true);
                OnWallDetected = false;
            }
        }
    }

    protected virtual void OnStick()
    {
        if (!collisionState.Standing && body2d.velocity.y > 0)
        {
            body2d.gravityScale = 0;
            body2d.drag = 100;
        }
    }

    protected virtual void OffWall()
    {
        if (body2d.gravityScale != defaultGravityScale)
        {
            body2d.gravityScale = defaultGravityScale;
            body2d.drag = defaultDrag;
        }
    }
}
