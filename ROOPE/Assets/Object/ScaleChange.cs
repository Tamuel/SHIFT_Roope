using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	public float scaleChange;
	public float scaleChangeSpeed;
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
		if (other.tag == "Player" && triggerOn == false) {
			triggerOn = true;
			collideWithCharacter ();
			StartCoroutine (ScaleChanger (other));
		}
	}

	IEnumerator ScaleChanger (Collider2D other)
	{
		float t = 0;
		float totalTime = 1;
		GetComponent<SpriteRenderer> ().enabled = false;
		initialScale = other.transform.localScale;
		targetScale = other.transform.localScale + new Vector3 (scaleChange, scaleChange, scaleChange);
		do {
			other.transform.localScale = Vector3.Lerp (initialScale, targetScale, t / totalTime); 
			yield return null;
			t += Time.deltaTime;
		} while (t < totalTime);

		yield return new WaitForSeconds (maintainTime);

		t = 0;
		do {
			other.transform.localScale = Vector3.Lerp (targetScale, initialScale, t / totalTime); 
			yield return null;
			t += Time.deltaTime;
		} while (t < totalTime);

		Destroy (gameObject);
	}

	public override void collideWithCharacter()
	{
	}

	public override RopeCollisionType collideWithRopeHead (Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	public override RopeCollisionType collideWithRopeLine (RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
}
