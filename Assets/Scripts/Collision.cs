using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, bottomRightOffset, bottomLeftOffset, topRightOffset, topLeftOffset;
    private Color debugCollisionColor = Color.red;

    // Update is called once per frame
    void Update()
    {  
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);

        onWall = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, collisionRadius, groundLayer) 
            || Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, collisionRadius, groundLayer)
			|| Physics2D.OverlapCircle((Vector2)transform.position + topRightOffset, collisionRadius, groundLayer)
			|| Physics2D.OverlapCircle((Vector2)transform.position + topLeftOffset, collisionRadius, groundLayer);

		onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + bottomRightOffset, collisionRadius, groundLayer)
			|| Physics2D.OverlapCircle((Vector2)transform.position + topRightOffset, collisionRadius, groundLayer);

        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + bottomLeftOffset, collisionRadius, groundLayer)
			|| Physics2D.OverlapCircle((Vector2)transform.position + topLeftOffset, collisionRadius, groundLayer);

		wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, bottomRightOffset, bottomLeftOffset, topRightOffset, topLeftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position  + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomRightOffset, collisionRadius);
		Gizmos.DrawWireSphere((Vector2)transform.position + bottomLeftOffset, collisionRadius);
		Gizmos.DrawWireSphere((Vector2)transform.position + topRightOffset, collisionRadius);
		Gizmos.DrawWireSphere((Vector2)transform.position + topLeftOffset, collisionRadius);
	}
}
