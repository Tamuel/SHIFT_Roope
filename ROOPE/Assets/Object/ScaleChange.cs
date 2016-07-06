using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	public float scaleChangeSize;
	public float scaleChangeTime;
	public float maintainTime;

	private Vector3 initialScale;
	private Vector3 targetScale; 
	private bool triggerOn; 

	void Start ()
	{
		triggerOn = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
	}

	IEnumerator ScaleChanger (Collider2D other)
	{
		float t = 0; // initialize time

		GetComponent<SpriteRenderer> ().enabled = false;

		initialScale = other.transform.localScale;
		targetScale = other.transform.localScale + new Vector3 (scaleChangeSize, scaleChangeSize, scaleChangeSize);

		// change player's scale larger
		do {
			other.transform.localScale = Vector3.Lerp (initialScale, targetScale, t / scaleChangeTime); 
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleChangeTime);

		yield return new WaitForSeconds (maintainTime);

		t = 0; // initialize time

		// change player's scale smaller
		do {
			other.transform.localScale = Vector3.Lerp (targetScale, initialScale, t / scaleChangeTime); 
			yield return null;
			t += Time.deltaTime;
		} while (t < scaleChangeTime);

		Destroy (gameObject);
	}

	public override void collideWithCharacter(Player player)
	{
		if (!triggerOn) {
			triggerOn = true;
			StartCoroutine (ScaleChanger (player.GetComponent<Collider2D>()));
		}
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
