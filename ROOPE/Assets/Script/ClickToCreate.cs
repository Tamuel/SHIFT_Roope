using UnityEngine;
using System.Collections;

public class ClickToCreate : MonoBehaviour {

	HingeJoint2D hd;

	public GameObject clickPointObject;
	private Transform clickPoint;
	private GameObject instantiatedObj;

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Destroy(instantiatedObj,0.5f);
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
			hd = GetComponent<HingeJoint2D> ();
			hd.connectedBody = instantiatedObj.GetComponent<Rigidbody2D>();
		}

	}
}
