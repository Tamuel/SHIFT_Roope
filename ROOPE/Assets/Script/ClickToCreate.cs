using UnityEngine;
using System.Collections;

public class ClickToCreate : MonoBehaviour {

	HingeJoint2D hinge;
	LineRenderer lineRenderer;

	public GameObject clickPointObject;
	public GameObject player;
	private Transform clickPoint;
	private GameObject instantiatedObj;

	float playerAndClickPointX;
	float playerAndClickPointY;



	void Start() {
		hinge = GetComponent<HingeJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			
			Debug.Log ("" + Input.mousePosition.ToString ());
			GameObject temp = new GameObject ();
			clickPoint = temp.transform;
			Destroy (temp);
			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			pos.z = 0;
			clickPoint.position = pos;
			Debug.Log ("" + clickPoint.position.ToString ()); 
			clickPoint.rotation = new Quaternion (0, 0, 0, 0);
			instantiatedObj = (GameObject) Instantiate (clickPointObject, clickPoint.position, clickPoint.rotation);
			hinge.connectedBody = instantiatedObj.GetComponent<Rigidbody2D>();

			playerAndClickPointX = player.transform.position.x - clickPoint.position.x;
			playerAndClickPointY = player.transform.position.y - clickPoint.position.y;

			Debug.Log ("Anchor: " + hinge.connectedAnchor.x + " " + hinge.connectedAnchor.y);
			hinge.connectedAnchor = new Vector2 (playerAndClickPointX, playerAndClickPointY);
		}

		if (Input.GetMouseButtonUp (0)) {
			if (lineRenderer != null) {
				lineRenderer.SetPosition (0, player.transform.position);
				lineRenderer.SetPosition (1, player.transform.position);
			}
			hinge.connectedAnchor = new Vector2 (0f, 0f);
			Destroy(instantiatedObj);
		}

		if (hinge.connectedAnchor.magnitude >= 0.1) {
			hinge.connectedAnchor = new Vector2 (hinge.connectedAnchor.x * 0.99f, hinge.connectedAnchor.y * 0.99f);
			if (lineRenderer != null) {
				lineRenderer.SetPosition (0, player.transform.position);
				if (clickPoint != null)
					lineRenderer.SetPosition (1, clickPoint.position);
			}
		}
			



	}
}
