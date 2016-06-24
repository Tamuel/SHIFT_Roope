using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	public float scaleChange;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			Destroy (gameObject);
			other.transform.localScale *= scaleChange;
		}
	}

	public override void collideWithCharacter()
	{

	}

	public override RopeCollisionType collideWithRopeHead () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine () {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
