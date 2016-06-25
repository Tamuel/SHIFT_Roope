using UnityEngine;
using System.Collections;

public class ScaleChange : Item
{
	public float scaleChange;
	public float scaleChangeSpeed;
	public float maintainTime;

	private Vector3 presentScale;
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
			//StartCoroutine (ScaleChanger ());
		}
	}

	IEnumerator ScaleChanger (Collider2D other)
	{
		GetComponent<MeshRenderer> ().enabled = false;
		presentScale = other.transform.localScale;
		other.transform.localScale = new Vector3 (presentScale.x + scaleChange, presentScale.y + scaleChange, presentScale.z + scaleChange);		yield return new WaitForSeconds (maintainTime);
		presentScale = other.transform.localScale;
		other.transform.localScale = new Vector3 (presentScale.x - scaleChange, presentScale.y - scaleChange, presentScale.z - scaleChange);

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
