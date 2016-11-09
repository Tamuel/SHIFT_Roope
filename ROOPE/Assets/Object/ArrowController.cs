using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowController : MonoBehaviour, Collision {

	public GameObject arrow;
    public AudioClip arrowShootClip;

	public int arrowCount; // number of Arrow
	public int startWait; // wait seconds when start 1 term of ArrowShoot
	public float shootWait; // interval of ArrowShoot
	public float endWait; // wait seconds after end of 1 term of ArrowShoot

	private bool isShoot;

	private float cameraWidth;

	public Text ArrowText;
	private float y_axis_position;


	private Vector3 shootPosition;

	void Start () 
	{
		isShoot = false;
		cameraWidth = 2f * Camera.main.orthographicSize * Camera.main.aspect;
		y_axis_position = 0;
	}

	public void setYAxis(float y) {
		y_axis_position = y;
	}

	// Action when collide with character
	public void collideWithCharacter(Player player) {
		if (!isShoot) {
			setYAxis (player.transform.position.y);
			ArrowText = Instantiate (ArrowText);
			ArrowText.transform.SetParent(FindObjectOfType<Canvas> ().transform);
			ArrowText.transform.position = new Vector3 (
				Camera.main.gameObject.transform.position.x + cameraWidth / 2 + 1.0f,
				y_axis_position,
				0);
			isShoot = true;
            SoundManager.instance.PlaySingle(arrowShootClip);
            StartCoroutine (ArrowShoot ());
		}
	}

	// Action when collide with rope head
	public RopeCollisionType collideWithRopeHead(Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	// Action when collide with rope line
	public RopeCollisionType collideWithRopeLine(RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	IEnumerator ArrowShoot ()
	{
		for (int i = startWait; i > 0; i--) {
			ArrowText.text = "" + i;
			yield return new WaitForSeconds (1);
		}
		ArrowText.text = "";
		yield return null;

		// 1 term of ArrowShoot
		for (int i = 0; i < arrowCount; i++) {
			// Arrow Position which changes with Camera Position
			shootPosition = ArrowText.transform.position;
			Quaternion shootRotation = Quaternion.identity;

			// Arrow Shoot
			Instantiate (arrow, shootPosition, shootRotation);

            yield return new WaitForSeconds (shootWait);
		}
		yield return new WaitForSeconds (endWait);
	}
}