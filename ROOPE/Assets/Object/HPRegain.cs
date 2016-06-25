using UnityEngine;
using System.Collections;

public class HPRegain : Item
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
		
			Destroy (gameObject);
			collideWithCharacter ();
		}
	}

    public override void collideWithCharacter()
    {
		FindObjectOfType<GameManager> ().addHP (1);
    }

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}