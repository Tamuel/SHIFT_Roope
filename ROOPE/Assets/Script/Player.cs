using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	HingeJoint2D hingeJoint2D;
	LineRenderer lineRenderer;

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



	void Start() {
		hingeJoint2D = GetComponent<HingeJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();

		rope1Prefab = Instantiate (rope);
		rope1Prefab.transform.parent = transform;
		rope1Prefab.transform.localScale = new Vector3 (2, 2, 2);
		rope1Prefab.GetComponent<Rope> ().setPlayer(this);

		Debug.Log (
			rope1Prefab.transform.position.ToString () + "\n" +
			rope1Prefab.transform.rotation.ToString () + "\n" +
			rope1Prefab.transform.localScale.ToString ()
		);

		rope2Prefab = Instantiate (rope);
		rope2Prefab.transform.parent = transform;
		rope2Prefab.transform.localScale = new Vector3 (2, 2, 2);
		rope2Prefab.GetComponent<Rope> ().setPlayer(this);

		Debug.Log (rope2Prefab.transform.position.ToString ());
	}

	void Update () {
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

		}

		if (Input.GetMouseButtonUp (0)) {
			ropeIsLaunched = false;
			rope1Prefab.GetComponent<Rope> ().stopRope ();

			if (lineRenderer != null) {
				lineRenderer.SetPosition (0, player.transform.position);
				lineRenderer.SetPosition (1, player.transform.position);
			}
			hingeJoint2D.connectedAnchor = new Vector2 (0f, 0f);
			Destroy(instantiatedObject);
		}

		if (hingeJoint2D.connectedAnchor.magnitude >= 0.1) {
			hingeJoint2D.connectedAnchor = new Vector2 (hingeJoint2D.connectedAnchor.x * 0.99f, hingeJoint2D.connectedAnchor.y * 0.99f);
			if (lineRenderer != null) {
				lineRenderer.SetPosition (0, player.transform.position);
				if (touchPointTransform != null)
					lineRenderer.SetPosition (1, touchPointTransform.position);
			}
		}
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