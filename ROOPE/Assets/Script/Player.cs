using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	HingeJoint2D[] hingeJoint2D;
	LineRenderer lineRenderer;

	public GameObject rope;

	private GameObject rope1Prefab;
	private GameObject rope2Prefab;

	private float relativeVectorFromTouchPointToPlayerX;
	private float relativeVectorFromTouchPointToPlayerY;

	private float[] shortestLength;
	private float[] curLength;



	void Start() {
		hingeJoint2D = GetComponents<HingeJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();

		shortestLength = new float[2];
		curLength = new float[2];

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
		if (rope1Prefab.GetComponent<Rope> ().isRopeAttached)
			getCentripetalAccel (ref curLength [0], ref shortestLength [0], rope1Prefab, this.gameObject);

		if (rope2Prefab.GetComponent<Rope> ().isRopeAttached)
			getCentripetalAccel (ref curLength [1], ref shortestLength [1], rope2Prefab, this.gameObject);
	}
		
	void Update () {

		// Pull Rope
		if (rope1Prefab.GetComponent<Rope> ().isRopeAttached)
			getToRopeForce (curLength[0], hingeJoint2D [0], rope1Prefab, this.gameObject);
		if (rope2Prefab.GetComponent<Rope> ().isRopeAttached)
			getToRopeForce (curLength[1], hingeJoint2D [1], rope2Prefab, this.gameObject);


		// Shoot Rope
		if (Input.touchCount >= 1) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				if (!rope1Prefab.GetComponent<Rope> ().isRopeLaunched)
					shootRope (ref curLength [0], ref shortestLength [0], rope1Prefab, hingeJoint2D [0], Input.GetTouch (0).position);
				else if (!rope2Prefab.GetComponent<Rope> ().isRopeLaunched)
					shootRope (ref curLength [1], ref shortestLength [1], rope2Prefab, hingeJoint2D [1], Input.GetTouch (0).position);
			} else if (Input.GetTouch (1).phase == TouchPhase.Began) {
				if (!rope1Prefab.GetComponent<Rope> ().isRopeLaunched)
					shootRope (ref curLength [0], ref shortestLength [0], rope1Prefab, hingeJoint2D [0], Input.GetTouch (1).position);
				else if (!rope2Prefab.GetComponent<Rope> ().isRopeLaunched)
					shootRope (ref curLength [1], ref shortestLength [1], rope2Prefab, hingeJoint2D [1], Input.GetTouch (1).position);
			}
		}


		// Stop Rope
		if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended &&
			rope1Prefab.GetComponent<Rope> ().isRopeLaunched)
			stopRope (rope1Prefab, hingeJoint2D [0]);
		if (Input.touchCount != 0 && Input.GetTouch(1).phase == TouchPhase.Ended &&
			rope2Prefab.GetComponent<Rope> ().isRopeLaunched)
			stopRope (rope2Prefab, hingeJoint2D [1]);
		if (Input.touchCount == 0) {
			stopRope (rope1Prefab, hingeJoint2D [0]);
			stopRope (rope2Prefab, hingeJoint2D [1]);
		}

		drawLine ();
	}

	void drawLine() {
		if (lineRenderer != null) {
			if (rope1Prefab.GetComponent<Rope> ().isRopeLaunched && rope2Prefab.GetComponent<Rope> ().isRopeLaunched) {
				lineRenderer.SetPosition (0, rope1Prefab.transform.position);
				lineRenderer.SetPosition (1, transform.position);
				lineRenderer.SetPosition (2, rope2Prefab.transform.position);
			} else if (rope1Prefab.GetComponent<Rope> ().isRopeLaunched) {
				lineRenderer.SetPosition (0, rope1Prefab.transform.position);
				lineRenderer.SetPosition (1, transform.position);
				lineRenderer.SetPosition (2, transform.position);
			} else if (rope2Prefab.GetComponent<Rope> ().isRopeLaunched) {
				lineRenderer.SetPosition (0, rope2Prefab.transform.position);
				lineRenderer.SetPosition (1, transform.position);
				lineRenderer.SetPosition (2, transform.position);
			} else {
				lineRenderer.SetPosition (0, transform.position);
				lineRenderer.SetPosition (1, transform.position);
				lineRenderer.SetPosition (2, transform.position);
			}
		}
	}

	void stopRope(GameObject rope, HingeJoint2D hinge) {
		rope.GetComponent<Rope> ().stopRope ();
		hinge.connectedAnchor = new Vector2 (0f, 0f);
	}

	void shootRope(ref float curLength, ref float shortestLength, GameObject rope, HingeJoint2D hinge, Vector3 touchPosition) {
		curLength = 0;
		shortestLength = 0;

		rope.GetComponent<Rope> ().launchRope (touchPosition);

		relativeVectorFromTouchPointToPlayerX = transform.position.x - rope.transform.position.x;
		relativeVectorFromTouchPointToPlayerY = transform.position.y - rope.transform.position.y;

		hinge.connectedAnchor = new Vector2 (
			relativeVectorFromTouchPointToPlayerX,
			relativeVectorFromTouchPointToPlayerY
		);
		shortestLength = hinge.connectedAnchor.magnitude;
	}

	// Get centripetal force
	private void getCentripetalAccel(ref float curLength, ref float shortestLength, GameObject centerObj, GameObject circuralObj) {
		curLength = (centerObj.transform.position - circuralObj.transform.position).magnitude;
		Rigidbody2D tempBody = circuralObj.GetComponent<Rigidbody2D> ();
		if (curLength >= shortestLength && curLength > 0.2f)
			tempBody.AddForce (
				(Vector2)(centerObj.transform.position - circuralObj.transform.position).normalized *
				tempBody.mass * Mathf.Pow (tempBody.velocity.magnitude, 2) /
				(centerObj.transform.position - circuralObj.transform.position).magnitude
				- tempBody.velocity * 5
			);
		else
			shortestLength = curLength;
	}

	// Get rope attraction force
	private void getToRopeForce(float curLength, HingeJoint2D hingeJoint2D, GameObject centerObj, GameObject circuralObj) {
		Rigidbody2D rigidBody2D = circuralObj.GetComponent<Rigidbody2D> ();

		hingeJoint2D.connectedBody = rigidBody2D;
		hingeJoint2D.connectedAnchor = new Vector2 (0f, 0f);

		Vector2 force;
		if(curLength > 0.2f)
			force = new Vector2(
				centerObj.transform.position.x - circuralObj.transform.position.x,
				centerObj.transform.position.y - circuralObj.transform.position.y
			).normalized * 30 * rigidBody2D.mass - rigidBody2D.velocity * 5;
		else
			force = new Vector2(
				centerObj.transform.position.x - circuralObj.transform.position.x,
				centerObj.transform.position.y - circuralObj.transform.position.y
			).normalized * 11 - rigidBody2D.velocity * 100;
		rigidBody2D.AddForce (force);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<RObject> () != null)
			other.GetComponent<RObject> ().collideWithCharacter ();
	}
}