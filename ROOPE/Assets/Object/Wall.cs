using UnityEngine;
using System.Collections;

public class Wall : Obstacle {

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}
	}

	public override void collideWithCharacter ()
	{

	}

}
