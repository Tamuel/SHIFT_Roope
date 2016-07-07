﻿using UnityEngine;
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

	// Use this for initialization
	void Start () {
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

		prevPosition = new Vector3 (
			transform.position.x + speed,
			transform.position.y,
			transform.position.z
		);

		distance = player.transform.position.x - startPosition.x;

		if (prevPosition.x < player.transform.position.x + centerOffset)
			prevPosition = new Vector3(
				player.transform.position.x + centerOffset,
				this.transform.position.y,
				this.transform.position.z
			);

		if (maxDistance < distance)
		{
			maxDistance = distance;
		}

		if ((int)maxDistance > distanceScore)
		{
			distanceScore = (int)maxDistance;
			FindObjectOfType<GameManager>().addScore(10);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.position = prevPosition;
	}
}