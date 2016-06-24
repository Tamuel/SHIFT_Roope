using UnityEngine;
using System.Collections;

public class ScoreItem : Item {

	public int score;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			Destroy (gameObject);
			collideWithCharacter ();
		}
	}

	public override void collideWithCharacter()
	{
		FindObjectOfType<GameManager> ().addScore (score);
	}

	public override RopeCollisionType collideWithRopeHead () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
