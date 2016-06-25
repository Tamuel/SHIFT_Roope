using UnityEngine;
using System.Collections;

public abstract class Obstacle : RObject {

	public float speed; // move speed
    private Rigidbody2D rb;

    public override void move (float delta_x, float delta_y)
    {
		if (isMovable ())
        {
			rb = GetComponent<Rigidbody2D>();
			rb.velocity = new Vector2(delta_x, delta_y) * speed;
        }
    }
}