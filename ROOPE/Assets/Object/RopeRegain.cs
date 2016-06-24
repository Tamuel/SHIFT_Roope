using UnityEngine;
using System.Collections;

public class RopeRegain : Item
{
	public int gainNumberOfRope;
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}
	}

    public override void collideWithCharacter()
    {
		FindObjectOfType<GameManager> ().addNumberOfRope (gainNumberOfRope);
    }

}