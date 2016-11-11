using UnityEngine;
using System.Collections;

public class ScoreItem : Item {

	public int score;

	private bool up;
	private float range = 0.3f;

	private Vector3 startPosition;

	void Start() {
		up = true;
		speed = Random.Range (20, 40);
		startPosition = transform.position;
	}

	void Update() {
		if (up) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + (startPosition.y + range - transform.position.y) / speed, 0);
			if (startPosition.y + range - transform.position.y < 0.1f)
				up = false;
		} else {
			transform.position = new Vector3 (transform.position.x, transform.position.y - (transform.position.y - (startPosition.y - range)) / speed, 0);
			if (transform.position.y - (startPosition.y - range) < 0.1f)
				up = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
	}

	// Score +score
	public override void collideWithCharacter(Player player)
	{
		FindObjectOfType<GameManager> ().addScore (score);
		Destroy (gameObject);
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
