using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	public float scaleChange;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			Destroy (gameObject);
			other.transform.localScale *= scaleChange;
		}
	}
}
