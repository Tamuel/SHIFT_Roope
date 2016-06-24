using UnityEngine;
using System.Collections;

public class ClickToCreate : MonoBehaviour {

	HingeJoint2D hingeJoint2D;
	LineRenderer lineRenderer;

	public GameObject touchPoint;
	public GameObject player;
	private Transform touchPointTransform;
	private GameObject instantiatedObject;

	float relativeVectorFromTouchPointToPlayerX;
	float relativeVectorFromTouchPointToPlayerY;



	void Start() {
		hingeJoint2D = GetComponent<HingeJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Debug.Log ("" + Input.mousePosition.ToString ());

			GameObject temp = new GameObject ();
			touchPointTransform = temp.transform;
			Destroy (temp);
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;
			touchPointTransform.position = pos;
			Debug.Log ("" + touchPointTransform.position.ToString ()); 
			touchPointTransform.rotation = new Quaternion (0, 0, 0, 0);
			instantiatedObject = (GameObject) Instantiate (touchPoint, touchPointTransform.position, touchPointTransform.rotation);
			hingeJoint2D.connectedBody = instantiatedObject.GetComponent<Rigidbody2D>();

			relativeVectorFromTouchPointToPlayerX = player.transform.position.x - touchPointTransform.position.x;
			relativeVectorFromTouchPointToPlayerY = player.transform.position.y - touchPointTransform.position.y;

			Debug.Log ("Anchor: " + hingeJoint2D.connectedAnchor.x + " " + hingeJoint2D.connectedAnchor.y);
			hingeJoint2D.connectedAnchor = new Vector2 (relativeVectorFromTouchPointToPlayerX, relativeVectorFromTouchPointToPlayerY);
		}

		if (Input.GetMouseButtonUp (0)) {
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
}
