/*
using UnityEngine;
using System.Collections;

public class web : MonoBehaviour {

	public GameObject grapplingHookPrefab;
	public GameObject currentGrapplingHook;
	public GameObject failedGrapplePrefab;
	public GameObject failedGrapple;

	private Vector3 grapplePoint = new Vector3 (0, 0, 0);

	public bool grappleDeployed = false;
	private float grappleRetractSpeed = 10f;
	private float grapplePayoutSpeed = 10f;
	private float ropeLength;
	private Vector3 ropeTension = Vector3.zero;
	private Vector3 directionToGrapple;
	public Vector3 customGravity;
	private int retracting;

	private bool clambering = false;
	private float clamberTolerance = 1f;
	private Vector3 clamberPoint = Vector3.zero;

	public Vector3 CursorPosition(){

		Ray rayFromCameraToMouse = Camera.main.ScreenPointToRay (Input.mousePosition);

		Plane zequalsZero = new Plane (new Vector3 (0, 0, -1), new Vector3 (0, 0, 0));

		float distanceFromCameraToZEqualsZero = 0f;

		zequalsZero.Raycast (rayFromCameraToMouse, out distanceFromCameraToZEqualsZero);

		return rayFromCameraToMouse.GetPoint (distanceFromCameraToZEqualsZero);
	}

	public void SetPopeLength(){
	
		ropeLength = Vector3.Distance (grapplePoint, transform.position);
	}

	void FixedUpdate () {
		customGravity = Physics.gravity * 60;

		Vector3 vectorToGrapple = grapplePoint - transform.position;

		float distanceToGrapple = vectorToGrapple.magnitude;

		directionToGrapple = vectorToGrappleToGrapple / distanceToGrapple;

		float speedTowardsAnchor = Vector3.Dot (GetComponent<Rigidbody>().velocity, directionToGrapple);

		if (grappleDeployed) {
		
			AddTension ();

		}

	}
}
*/