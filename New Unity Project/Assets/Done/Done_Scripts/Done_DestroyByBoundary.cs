using UnityEngine;
using System.Collections;

public class Done_DestroyByBoundary : MonoBehaviour
{
	HingeJoint2D hd;

	void OnTriggerExit (Collider other) 
	{
		hd = GetComponent<HingeJoint2D> ();
		hd.anchor.y;
		Destroy(other.gameObject);
	}
}