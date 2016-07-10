using UnityEngine;
using System.Collections;

public class Wall : Obstacle {

	public bool canDrop;
    public int direction;

	private Rope rope;
    private Rigidbody2D rb;

    void Start ()
    {
        moveWall(direction);
    }

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Arrow") {
			Destroy (other.gameObject);
		}
	}

    public void moveWall (float direction)
    {
        // left
        if (direction == 0)
            move(-1, 0);
        // up
        else if (direction == 1)
            move(0, 1);
        // right
        else if (direction == 2)
            move(1, 0);
        // down
        else if (direction == 3)
            move(0, -1);
    }

    // HP -1
    public override void collideWithCharacter (Player player)
	{
		FindObjectOfType<GameManager> ().addHP (-1);
	}

	// if canDrop is true in Rope, Wall falls
	public void dropWall() 
	{
		GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	public override RopeCollisionType collideWithRopeHead(Rope rope) {
		this.rope = rope;
		if (isRopeAttachable () && !canDrop)
			return RopeCollisionType.CAN_ATTACH;
		else if (isRopeAttachable() && canDrop) {
			dropWall ();
			return RopeCollisionType.CAN_ATTACH_AND_DROP;
		} else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}


	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}
