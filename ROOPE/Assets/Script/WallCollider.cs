using UnityEngine;
using System.Collections;

public class WallCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.GetComponent<Wall> () != null && other.GetComponent<Wall> ().isMovable ()) {
			other.GetComponent<Wall> ().GetComponent<Rigidbody2D> ().velocity =
				(transform.position - other.transform.position) * other.GetComponent<Wall> ().speed;

			if ((other.transform.position - transform.position).magnitude <= 1)
				other.GetComponent<Wall> ().movable = false;
		}
	}
}
