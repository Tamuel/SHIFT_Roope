using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	void onTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {
			Debug.Log ("AAAA");
		}
	}
}
