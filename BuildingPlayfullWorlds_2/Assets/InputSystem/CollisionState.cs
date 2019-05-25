using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
    public LayerMask CollisionLayer;
    public bool Standing;
    public bool OnWall;
    public Vector2 BottomPosition = Vector2.zero;
    public Vector2 RightPosition = Vector2.zero;
    public Vector2 LeftPosition = Vector2.zero;
    public float CollisionRadius = 10f;
    public Color DebugCollisionColor = Color.red;

    private InputState inputState;

	// Use this for initialization
	void Awake ()
	{
	    inputState = GetComponent<InputState>();
	}

    private void FixedUpdate()
    {
        var pos = BottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        Standing = Physics2D.OverlapCircle(pos, CollisionRadius, CollisionLayer);

        pos = inputState.Direction == Directions.Right ? RightPosition : LeftPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        OnWall = Physics2D.OverlapCircle(pos, CollisionRadius, CollisionLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = DebugCollisionColor;

        var positions = new Vector2[] {RightPosition, BottomPosition, LeftPosition};

        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere(pos, CollisionRadius);
        }
    }

}
