using UnityEngine;
using System.Collections;

public class Arrow : Obstacle {

    public AudioClip damagedClip;

	void Start ()
	{
		move (-1, 0);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
	}

	// HP -1
	public override void collideWithCharacter(Player player) {
        EffectMusicManager.instance.PlaySingle(damagedClip);
		FindObjectOfType<GameManager> ().addHP (-1);
		player.rope1Prefab.GetComponent<Rope> ().stopRope ();
		player.rope2Prefab.GetComponent<Rope> ().stopRope ();
		player.GetComponent<Rigidbody2D> ().AddForce (new Vector2(-16000, 4200));
		Debug.Log ("HP : " + FindObjectOfType<GameManager> ().getHP ());
		Destroy (gameObject);
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}