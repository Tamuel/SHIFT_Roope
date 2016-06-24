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
}
