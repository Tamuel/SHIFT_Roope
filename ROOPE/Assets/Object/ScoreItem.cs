using UnityEngine;
using System.Collections;

public class ScoreItem : Item {

	public int score; 

	void OnTriggerEnter2D (Collider2D other)
	{
	}

	// Score +score
	public override void collideWithCharacter(Player player)
	{
		FindObjectOfType<GameManager> ().addScore (score);
		Destroy (gameObject);
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
