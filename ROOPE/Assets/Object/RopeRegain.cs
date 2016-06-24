using UnityEngine;
using System.Collections;

public class RopeRegain : Item
{
	public int gainNumberOfRope;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
			Destroy (gameObject);
		}
	}

    public override void collideWithCharacter()
    {
		FindObjectOfType<GameManager> ().addNumberOfRope (gainNumberOfRope);
    }

	public override RopeCollisionType collideWithRopeHead () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}