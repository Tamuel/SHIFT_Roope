using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Player player;
	public float speed;

	private Vector3 prevPosition;
	private Vector3 startPoint;
	private Vector3 playerPosition;
	private float centerOffset = 3;

	// Use this for initialization
	void Start () {
		prevPosition = new Vector3(
			player.transform.position.x + centerOffset,
			this.transform.position.y,
			this.transform.position.z
		);

		startPoint = FindObjectOfType<Player> ().transform.position;
	}

	// Update Camera position when character move forward then before
	void FixedUpdate() {
		if (prevPosition.x < player.transform.position.x + centerOffset)
			prevPosition = new Vector3(
				player.transform.position.x + centerOffset,
				this.transform.position.y,
				this.transform.position.z
			);

		prevPosition = new Vector3 (
			transform.position.x + speed,
			transform.position.y,
			transform.position.z
		);
	}

	// Update is called once per frame
	void Update () {
		transform.position = prevPosition;
	}
}