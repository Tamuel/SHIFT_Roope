using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {

	void OnTriggerEnter (Collider2D other)
	{
		Destroy (other.gameObject);
	}
}
