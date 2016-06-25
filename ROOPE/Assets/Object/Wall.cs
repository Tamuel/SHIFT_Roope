using UnityEngine;
using System.Collections;

public class Wall : Obstacle {

	public bool canDrop;
	public Rope rope;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}

	}

	// HP -1
	public override void collideWithCharacter ()
	{
		FindObjectOfType<GameManager> ().addHP (-1);
	}

	// if canDrop is true in Rope, Wall falls
	public void dropWall() 
	{
		GetComponent<Rigidbody2D> ().isKinematic = false;
	}

	public override RopeCollisionType collideWithRopeHead(Rope rope){//Rope rope) {
		this.rope = rope;
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else if (canDrop) {
			dropWall ();
			return RopeCollisionType.CAN_ATTACH_AND_DROP;
		} else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}


	public override RopeCollisionType collideWithRopeLine (RopeLine line){//Line line) {
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}
