using UnityEngine;
using System.Collections;

public class Arrow : Obstacle {

	void Start ()
	{
		move (-1, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Destroy (gameObject);
			collideWithCharacter ();
		}

		if (other.tag == "Barrier") {
			Destroy (gameObject);
		}
	}

	public override void collideWithCharacter() {
		FindObjectOfType<GameManager> ().addHP (-1);
		Debug.Log ("HP : " + FindObjectOfType<GameManager> ().getHP ());
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}