using UnityEngine;
using System.Collections;

public class RopeRegain : Item
{
	public int gainNumberOfRope;
	void OnTriggerEnter2D (Collider2D other)
	{
	}

	// number of Rope +gainNumberOfRope
	public override void collideWithCharacter(Player player)
    {
		FindObjectOfType<GameManager> ().addNumberOfRope (gainNumberOfRope);
    }

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}