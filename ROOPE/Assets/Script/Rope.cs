using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	Transform target;
	public RopeLine ropeLine;
	public bool isRopeLaunched;
	public bool isRopeAttached;

	private Player player;
	private float speed;
	private Vector3 touchPosition;
	private Vector2 moveVector;

	private GameObject colideObject;

	private RopeCollisionType collisionType;


	void Awake () {
		enabled = false;
		isRopeLaunched = false;
		isRopeAttached = false;
		colideObject = null;
		collisionType = RopeCollisionType.NONE;
		speed = 100;
		Debug.Log ("Rope Start!");
	}


	void Update () {
		if (isRopeLaunched == true && collisionType == RopeCollisionType.NONE) {
			GetComponent<Rigidbody2D> ().velocity = moveVector; 
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<RObject> () != null) {
			Debug.Log ("Collide with object");
			RopeCollisionType col = other.GetComponent<RObject> ().collideWithRopeHead (this);
			Debug.Log (col.ToString());
			switch (col) {
			case RopeCollisionType.CAN_ATTACH:
				colideObject = other.gameObject;
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isRopeAttached = true;
				collisionType = RopeCollisionType.CAN_ATTACH;
				break;

			case RopeCollisionType.CAN_ATTACH_AND_DROP:
				colideObject = other.gameObject;
				collisionType = RopeCollisionType.CAN_ATTACH_AND_DROP;
				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_CUT:
				colideObject = other.gameObject;
				collisionType = RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH:
				colideObject = other.gameObject;
				collisionType = RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
				break;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.Equals (colideObject)) {
			Debug.Log ("Trigger Exit");
			collisionType = RopeCollisionType.NONE;
		}
	}

	public bool launchRope(Vector3 touchPosition) {
		if (!isRopeLaunched) {
			Debug.Log ("Launch Rope!");
			isRopeLaunched = true;
			isRopeAttached = false;

			touchPosition = Camera.main.ScreenToWorldPoint (touchPosition);
			// Shoot rope
			moveVector = new Vector2 (
				touchPosition.x - transform.position.x,
				touchPosition.y - transform.position.y
			).normalized * 20;
			transform.parent = null;
			enabled = true;

			return true;
		} else {
			
			return false;
		}
			
	}

	public void stopRope() {
		if (isRopeLaunched) {
			isRopeLaunched = false;
			isRopeAttached = false;
			colideObject = null;
			enabled = false;
			collisionType = RopeCollisionType.NONE;
			transform.position = player.transform.position;
			transform.parent = player.transform;
			transform.localScale = new Vector3 (4, 4, 4);
		}
	}
		
	public void setPlayer(Player player) {
		this.player = player;
	}

}
