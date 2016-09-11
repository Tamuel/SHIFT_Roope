using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, Wind {

	HingeJoint2D[] hingeJoint2D;

	public GameObject rope;

	public GameObject rope1Prefab;
	public GameObject rope2Prefab;

	private float relativeVectorFromTouchPointToPlayerX;
	private float relativeVectorFromTouchPointToPlayerY;

    private const float maxSpeed = 12;

    private float[] shortestLength;
	private float[] curLength;

	void Start() {
		hingeJoint2D = GetComponents<HingeJoint2D> ();
		shortestLength = new float[2];
		curLength = new float[2];

		rope1Prefab = Instantiate (rope);
		rope1Prefab.transform.parent = transform;
		rope1Prefab.GetComponent<Rope> ().setPlayer(this);
		rope1Prefab.GetComponent<Rigidbody2D> ().isKinematic = true;

		rope2Prefab = Instantiate (rope);
		rope2Prefab.transform.parent = transform;
		rope2Prefab.GetComponent<Rope> ().setPlayer(this);
		rope2Prefab.GetComponent<Rigidbody2D> ().isKinematic = true;


		CircleCollider2D collider1 = rope1Prefab.GetComponent <CircleCollider2D> ();
		CircleCollider2D collider2 = rope2Prefab.GetComponent <CircleCollider2D> ();

		Physics2D.IgnoreCollision (
			collider1,
			collider2,
			true
		);
			
		GameObject[] objects = GetComponent<Blob> ().getReferencePoints ();
		for(int i = 0; i < objects.Length; i++) {
			Physics2D.IgnoreCollision (
				collider1,
				objects[i].GetComponent <CircleCollider2D> (),
				true
			);
			Physics2D.IgnoreCollision (
				collider2,
				objects[i].GetComponent <CircleCollider2D> (),
				true
			);
		}
	}

	void FixedUpdate() {

		if (rope1Prefab.GetComponent<Rope> ().isRopeAttached && rope2Prefab.GetComponent<Rope> ().isRopeAttached)
			GetComponent<Rigidbody2D> ().gravityScale = 0;
		else
			GetComponent<Rigidbody2D> ().gravityScale = 1.5f;
	}
		
	void Update () {

		/* For mouse testing 
		if (Input.GetMouseButtonDown (0)) {
			shootRope (ref curLength [0], ref shortestLength [0],rope1Prefab, hingeJoint2D [0], Input.mousePosition);
		}

		if (Input.GetMouseButtonUp (0)) {
			stopRope (rope1Prefab, hingeJoint2D [0]);
		}
		*/

		/* Shoot Rope */
		if (Input.touchCount >= 1) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				if (!rope1Prefab.GetComponent<Rope> ().isRopeLaunched)
					shootRope (ref curLength [0], ref shortestLength [0],rope1Prefab, hingeJoint2D [0], Input.GetTouch (0).position);
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

	}

	void OnCollisionEnter2D(Collision2D collision) {
		Instantiate (Resources.Load ("Prefabs/RopeAttachedParticle"), collision.contacts[0].point, new Quaternion());
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

	void OnTriggerEnter2D (Collider2D other) {
		if (other.GetComponent<Collision> () != null)
			other.GetComponent<Collision> ().collideWithCharacter (this);
	}

	// Wind blow with force
	public void wind(float force_x, float force_y) {
		Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D> ();
		rigidBody2D.AddForce (new Vector2 (force_x, force_y));

		// Clamp Speed
		if (rigidBody2D.velocity.magnitude > maxSpeed) {
			float bias = maxSpeed / rigidBody2D.velocity.magnitude;
			rigidBody2D.velocity = new Vector2 (
				rigidBody2D.velocity.x * bias,
				rigidBody2D.velocity.y * bias
			);
		}
	}
}