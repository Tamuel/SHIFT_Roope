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

    private const float maxSpeed = 10;

    private RopeCollisionType collisionType;

    private float curLength;
    private float shortestLength;


	void Awake () {
		enabled = false;
		isRopeLaunched = false;
		isRopeAttached = false;
		lineRenderer = GetComponent<LineRenderer> ();
		colideObject = null;
		collisionType = RopeCollisionType.NONE;
		Debug.Log ("Rope Start!");
	}

    void Start() {
    }


    void FixedUpdate() {

        if(isRopeLaunched == false) {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }

        else {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }

        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());

        if (isRopeAttached)
            giveCentripetalAccelToPlayer(ref curLength, ref shortestLength, this.gameObject, player.gameObject);

    }


	void Update () {

		if (isRopeAttached) {
			if(collisionType == RopeCollisionType.CAN_ATTACH)
				getRopePullForce (curLength, player.GetComponent<HingeJoint2D> (), this.gameObject, player.gameObject, 40);
			if (collisionType == RopeCollisionType.CAN_ATTACH_AND_DROP) {
				getRopePullForce (curLength, player.GetComponent<HingeJoint2D> (), colideObject, player.gameObject, 20);
				getRopePullForce (curLength, null, player.gameObject, colideObject, 20);
			}
		}


        if (isRopeLaunched) {
			lineRenderer.SetPosition (0, transform.position); 
			lineRenderer.SetPosition (1, player.transform.position);
			// Rope fly
			if (collisionType == RopeCollisionType.NONE)
				GetComponent<Rigidbody2D> ().velocity = moveVector;
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.collider.GetComponent<RObject> () != null && isRopeLaunched) {
			Debug.Log ("Collide with object");
			RopeCollisionType col = other.collider.GetComponent<RObject> ().collideWithRopeHead (this);
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
				isRopeAttached = true;
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

	void OnCollisionExit2D(Collision2D other) {
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

    // Give centripetal force to player
    private void giveCentripetalAccelToPlayer(ref float curLength, ref float shortestLength, GameObject centerObj, GameObject circuralObj)
    {
        curLength = (centerObj.transform.position - circuralObj.transform.position).magnitude;
        Rigidbody2D tempBody = circuralObj.GetComponent<Rigidbody2D>();
        if (curLength >= shortestLength && curLength > 0.2f)
            tempBody.AddForce(
                (Vector2)(centerObj.transform.position - circuralObj.transform.position).normalized *
                tempBody.mass * Mathf.Pow(tempBody.velocity.magnitude, 2) /
                (centerObj.transform.position - circuralObj.transform.position).magnitude
                - tempBody.velocity * 5
            );
        else
            shortestLength = curLength;
    }

    // Get rope attraction force
	private void getRopePullForce(float curLength, HingeJoint2D hingeJoint2D, GameObject centerObj, GameObject circuralObj, float strength)
    {
        Rigidbody2D rigidBody2D = circuralObj.GetComponent<Rigidbody2D>();
		if (hingeJoint2D != null) {
			hingeJoint2D.connectedBody = rigidBody2D;
			hingeJoint2D.connectedAnchor = new Vector2 (0f, 0f);
		}

        Vector2 force;
        if (curLength > 0.3f)
            force = new Vector2(
                centerObj.transform.position.x - circuralObj.transform.position.x,
                centerObj.transform.position.y - circuralObj.transform.position.y
			).normalized * strength * rigidBody2D.mass - rigidBody2D.velocity * 5;
        else
            force = new Vector2(
                centerObj.transform.position.x - circuralObj.transform.position.x,
                centerObj.transform.position.y - circuralObj.transform.position.y
            ).normalized * 11 - rigidBody2D.velocity * 100;

        rigidBody2D.AddForce(force);

        // Clamp Speed
        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            float bias = maxSpeed / rigidBody2D.velocity.magnitude;
            rigidBody2D.velocity = new Vector2(
                rigidBody2D.velocity.x * bias,
                rigidBody2D.velocity.y * bias
            );
        }
    }

}
