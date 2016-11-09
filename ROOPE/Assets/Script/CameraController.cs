using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Player player;
	public float speed;

	private Vector3 prevPosition;
	private Vector3 startPosition;
	private Vector3 playerPosition;
	private float distance;
	private float maxDistance;
	private float centerOffset = 3;
	private int distanceScore;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		prevPosition = new Vector3(
			player.transform.position.x + centerOffset,
			this.transform.position.y,
			this.transform.position.z
		);

		startPosition = player.transform.position;
		maxDistance = 0.0f;
	}

	// Update Camera position when character move forward then before
	void FixedUpdate() {

		if (maxDistance / 10000 < 0.08)
		{
			prevPosition = new Vector3(
			    transform.position.x + speed + maxDistance / 10000,
			    transform.position.y,
			    transform.position.z
		    );
		}
		else
		{
			prevPosition.x = transform.position.x + speed + 0.08f;
		}

	}

	// Update is called once per frame
	void Update () {
        if (player)
        {
            distance = player.transform.position.x - startPosition.x;

            if (prevPosition.x < player.transform.position.x + centerOffset)
                prevPosition = new Vector3(
                    player.transform.position.x + centerOffset,
                    this.transform.position.y,
                    this.transform.position.z
                );
        }

		if (maxDistance < distance)
		{
			maxDistance = distance;
		}

		if ((int)maxDistance > distanceScore)
		{
			distanceScore = (int)maxDistance;
			gameManager.addScore(10);
		}

		transform.position = prevPosition;
	}
}