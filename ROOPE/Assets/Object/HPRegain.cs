using UnityEngine;
using System.Collections;

public class HPRegain : Item
{
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
		
			collideWithCharacter ();
		}
	}

    public override void collideWithCharacter()
    {
		FindObjectOfType<GameManager> ().addHP (1);
    }

}