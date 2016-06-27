using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public GameObject arrow;

	public int arrowCount; // number of Arrow
	public float startWait; // wait seconds when start 1 term of ArrowShoot
	public float shootWait; // interval of ArrowShoot
	public float endWait; // wait seconds after end of 1 term of ArrowShoot

	public Text ArrowText;
	private float y_axis_position;


	private Vector3 shootPosition;

	void Start () 
	{
		StartCoroutine (ArrowShoot ());
	}

	IEnumerator ArrowShoot ()
	{
		while (true) {

			y_axis_position = Random.Range (-3.5f, 3.5f);

			for (int i = 3; i > 0; i--) {
				ArrowText.transform.position = new Vector3 (
					Camera.main.gameObject.transform.position.x + 9,
					y_axis_position,
					0);
				ArrowText.text = "" + i;
				yield return new WaitForSeconds (1);
			}
			ArrowText.text = "";
			yield return null;

			// 1 term of ArrowShoot
			for (int i = 0; i < arrowCount; i++) {
				// Arrow Position which changes with Camera Position
				shootPosition = ArrowText.transform.position;
//				Vector3 shootPosition = new Vector3 (Camera.main.gameObject.transform.position.x + range.x, Random.Range(-range.y, range.y), range.z);
				Quaternion shootRotation = Quaternion.identity;
				// Arrow Shoot
				Instantiate (arrow, shootPosition, shootRotation);
				yield return new WaitForSeconds (shootWait);
			}
			yield return new WaitForSeconds (endWait);
		}
	}
}