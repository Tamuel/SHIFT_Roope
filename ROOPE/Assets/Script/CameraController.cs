using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	private Vector3 position;
	private Vector3 speed;

	// Use this for initialization
	void Start () {
		position = transform.position;
		speed = new Vector3 (0.05f, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		transform.position = transform.position + speed;
	}
}