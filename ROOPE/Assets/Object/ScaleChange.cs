using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}
	}

    public override void collideWithCharacter()
    {

    }
}
