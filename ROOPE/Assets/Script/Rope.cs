using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	Transform target;
	public RopeLine ropeLine;
	public bool isRopeLaunched;
	public bool isRopeAttached;

	private Player player;
	private LineRenderer lineRenderer;

	private float speed = 30;
	private Vector3 touchPosition;
	private Vector2 moveVector;

	private GameObject colideObject;

	private RopeCollisionType collisionType;


	void Awake () {
		enabled = false;
		isRopeLaunched = false;
		isRopeAttached = false;
		lineRenderer = GetComponent<LineRenderer> ();
		colideObject = null;
		collisionType = RopeCollisionType.NONE;
		Debug.Log ("Rope Start!");
	}


	void Update () {
		if (isRopeLaunched) {
			lineRenderer.SetPosition (0, transform.position); 
			lineRenderer.SetPosition (1, player.transform.position);
			// Rope fly
			if (collisionType == RopeCollisionType.NONE)
				GetComponent<Rigidbody2D> ().velocity = moveVector;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<RObject> () != null && isRopeLaunched) {
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
				transform.parent = other.transform;
				collisionType = RopeCollisionType.CAN_ATTACH_AND_DROP;
				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_CUT:
				colideObject = other.gameObject;
				stopRope ();
				collisionType = RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH:
				colideObject = other.gameObject;
				collisionType = RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
				break;
			}
		}

		if (!isRopeLaunched)
			collisionType = RopeCollisionType.NONE;
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
			).normalized * speed;
			transform.parent = null;
			enabled = true;

			return true;
		} else {
			
			return false;
		}
			
	}

	public void stopRope() {
		isRopeLaunched = false;
		isRopeAttached = false;
		colideObject = null;
		enabled = false;
		lineRenderer.SetPosition (0, Vector3.zero);
		lineRenderer.SetPosition (1, Vector3.zero);
		collisionType = RopeCollisionType.NONE;
		transform.position = player.transform.position;
		transform.parent = player.transform;
		transform.localScale = new Vector3 (4, 4, 4);
	}
		
	public void setPlayer(Player player) {
		this.player = player;
	}

}
