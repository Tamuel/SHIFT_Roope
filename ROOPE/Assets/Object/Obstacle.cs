using UnityEngine;
using System.Collections;

public class Obstacle : RObject, Collision, Move {

    public float speed;
    private Rigidbody2D rb;
	public Vector3 range;

    public virtual void collideWithCharacter ()
	{

	}



    public void move (float delta_x, float delta_y)
    {
        if (movable == true)
        {
			rb = GetComponent<Rigidbody2D>();
			transform.position = new Vector3 (Camera.main.gameObject.transform.position.x + range.x, Random.Range(-range.y, range.y), range.z);
			rb.velocity = new Vector2(delta_x, delta_y) * speed;
        }
    }
}