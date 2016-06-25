using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	HingeJoint2D hingeJoint2D;
	LineRenderer lineRenderer;
	Rigidbody2D rigidBody2D;

	public GameObject rope;

	public GameObject touchPoint;
	public GameObject player;

	private GameObject rope1Prefab;
	private GameObject rope2Prefab;
	private Transform touchPointTransform;
	private GameObject instantiatedObject;

	private float relativeVectorFromTouchPointToPlayerX;
	private float relativeVectorFromTouchPointToPlayerY;

	private bool ropeIsLaunched = false;

	private float shortestLength;
	private float curLength;



	void Start() {
		hingeJoint2D = GetComponent<HingeJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();

		rope1Prefab = Instantiate (rope);
		rope1Prefab.transform.parent = transform;
		rope1Prefab.transform.localScale = new Vector3 (4, 4, 4);
		rope1Prefab.GetComponent<Rope> ().setPlayer(this);

		Debug.Log (
			rope1Prefab.transform.position.ToString () + "\n" +
			rope1Prefab.transform.rotation.ToString () + "\n" +
			rope1Prefab.transform.localScale.ToString ()
		);

		rope2Prefab = Instantiate (rope);
		rope2Prefab.transform.parent = transform;
		rope2Prefab.transform.localScale = new Vector3 (4, 4, 4);
		rope2Prefab.GetComponent<Rope> ().setPlayer(this);

		Debug.Log (rope2Prefab.transform.position.ToString ());
	}

	void FixedUpdate() {
		curLength = (instantiatedObject.transform.position - transform.position).magnitude;
		if (curLength >= shortestLength && curLength > 0.2f)
			rigidBody2D.AddForce (
				(Vector2)(instantiatedObject.transform.position - transform.position).normalized *
				rigidBody2D.mass * Mathf.Pow (rigidBody2D.velocity.magnitude, 2) /
				(instantiatedObject.transform.position - transform.position).magnitude
				- rigidBody2D.velocity * 5
			);
		else
			shortestLength = curLength;
	}

	void Update () {

		if (ropeIsLaunched) {
			hingeJoint2D.connectedBody = rigidBody2D;
			hingeJoint2D.connectedAnchor = new Vector2 (0f, 0f);

			Vector2 force;
			if(curLength > 0.2f)
				force = new Vector2(
					instantiatedObject.transform.position.x - transform.position.x,
					instantiatedObject.transform.position.y - transform.position.y
				).normalized * 40 * rigidBody2D.mass - rigidBody2D.velocity * 5;
			else
				force = new Vector2(
					instantiatedObject.transform.position.x - transform.position.x,
					instantiatedObject.transform.position.y - transform.position.y
				).normalized * 11 - rigidBody2D.velocity * 100;
			rigidBody2D.AddForce (force);
		}

		if (Input.GetMouseButtonDown (0) && !ropeIsLaunched) {
			ropeIsLaunched = true;

			GameObject temp = new GameObject ();
			touchPointTransform = temp.transform;
			Destroy (temp);

			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;
			touchPointTransform.position = pos;

			touchPointTransform.rotation = new Quaternion (0, 0, 0, 0);

			instantiatedObject = (GameObject) Instantiate (touchPoint, touchPointTransform.position, touchPointTransform.rotation);
			hingeJoint2D.connectedBody = instantiatedObject.GetComponent<Rigidbody2D>();

			rope1Prefab.GetComponent<Rope> ().launchRope ();
//			if (!rope1Prefab.GetComponent<Rope> ().launchRope ())
//				rope2Prefab.GetComponent<Rope> ().launchRope ();


			relativeVectorFromTouchPointToPlayerX = player.transform.position.x - touchPointTransform.position.x;
			relativeVectorFromTouchPointToPlayerY = player.transform.position.y - touchPointTransform.position.y;

			Debug.Log ("Anchor: " + hingeJoint2D.connectedAnchor.x + " " + hingeJoint2D.connectedAnchor.y);
			hingeJoint2D.connectedAnchor = new Vector2 (relativeVectorFromTouchPointToPlayerX, relativeVectorFromTouchPointToPlayerY);
			shortestLength = hingeJoint2D.connectedAnchor.magnitude;
		}

		if (Input.GetMouseButtonUp (0) && ropeIsLaunched) {
			ropeIsLaunched = false;
			rope1Prefab.GetComponent<Rope> ().stopRope ();

			if (lineRenderer != null) {
				lineRenderer.SetPosition (0, player.transform.position);
				lineRenderer.SetPosition (1, player.transform.position);
			}
			hingeJoint2D.connectedAnchor = new Vector2 (0f, 0f);
			Destroy(instantiatedObject);
		}


//		if (hingeJoint2D.connectedAnchor.magnitude >= 0.1) {
//			hingeJoint2D.connectedAnchor = new Vector2 (hingeJoint2D.connectedAnchor.x * 0.97f, hingeJoint2D.connectedAnchor.y * 0.97f);
//			flySpeed += -hingeJoint2D.connectedAnchor / 50;
//			if (lineRenderer != null) {
//				lineRenderer.SetPosition (0, player.transform.position);
//				if (touchPointTransform != null)
//					lineRenderer.SetPosition (1, touchPointTransform.position);
//			}
//		}
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<RObject> () != null) {
			switch (other.GetComponent<RObject> ().collideWithRopeHead (null)) {
			case RopeCollisionType.CAN_ATTACH:

				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_CUT:

				break;

			case RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH:

				break;

			}
		}
	}
}