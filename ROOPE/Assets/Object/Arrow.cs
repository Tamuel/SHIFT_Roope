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

	public override void collideWithCharacter()
	{
		GameManager g = new GameManager ();
		float hp = g.getHP ();
		g.setHP (--hp);
		Debug.Log (g.getHP ());
	}

}