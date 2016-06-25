using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	Transform target;
	public RopeLine ropeLine;

	private Player player;
	private bool ropeLaunched;
	private float speed;
	private Vector3 vec3MousePosition;
	private Vector2 moveVector;


	void Start () {
		enabled = false;
		ropeLaunched = false;
		speed = 100;
		Debug.Log ("Rope Start!");
	}


	void Update () {
		
		if (ropeLaunched == true) {
			GetComponent<Rigidbody2D> ().velocity = moveVector; 
		}
	}

	public bool launchRope() {
		if (!ropeLaunched) {
			ropeLaunched = true;

			// Shoot rope
			vec3MousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			moveVector = new Vector2 (
				vec3MousePosition.x - transform.position.x,
				vec3MousePosition.y - transform.position.y
			).normalized * 20;
			transform.parent = null;
			enabled = true;

			return true;
		} else {
			
			return false;
		}
			
	}

	public void stopRope() {
		if (ropeLaunched) {
			ropeLaunched = false;
			enabled = false;
			transform.position = player.transform.position;
			transform.parent = player.transform;
			transform.localScale = new Vector3 (2, 2, 2);
		}
	}
		
	public void setPlayer(Player player) {
		this.player = player;
	}

}
