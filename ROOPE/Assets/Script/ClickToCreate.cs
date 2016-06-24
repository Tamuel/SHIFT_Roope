using UnityEngine;
using System.Collections;

public class ClickToCreate : MonoBehaviour {

	HingeJoint2D hd;
	LineRenderer lr;

	public GameObject clickPointObject;
	public GameObject player;
	private Transform clickPoint;
	private GameObject instantiatedObj;

	float a;
	float b;



	void Start() {
		hd = GetComponent<HingeJoint2D> ();
		lr = GetComponent<LineRenderer> ();
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
			hd.connectedBody = instantiatedObj.GetComponent<Rigidbody2D>();

			a = player.transform.position.x - clickPoint.position.x;
			b = player.transform.position.y - clickPoint.position.y;

			Debug.Log ("Anchor: " + hd.connectedAnchor.x + " " + hd.connectedAnchor.y);
			hd.connectedAnchor = new Vector2 (a, b);
		}

		if (Input.GetMouseButtonUp (0)) {
			lr.SetPosition (0, player.transform.position);
			lr.SetPosition (1, player.transform.position);
			hd.connectedAnchor = new Vector2 (0f, 0f);
			Destroy(instantiatedObj);
		}

		if (hd.connectedAnchor.magnitude >= 0.1) {
			hd.connectedAnchor = new Vector2 (hd.connectedAnchor.x * 0.99f, hd.connectedAnchor.y * 0.99f);
			lr.SetPosition (0, player.transform.position);
			lr.SetPosition (1, clickPoint.position);
		}
			



	}
}
