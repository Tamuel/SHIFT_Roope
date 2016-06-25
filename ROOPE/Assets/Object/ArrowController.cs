using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public GameObject arrow;
	public Vector3 range;

	public int arrowCount; // number of Arrow
	public float startWait; // wait seconds when start 1 term of ArrowShoot
	public float shootWait; // interval of ArrowShoot
	public float endWait; // wait seconds after end of 1 term of ArrowShoot

	void Start () 
	{
		StartCoroutine (ArrowShoot ());
	}

	IEnumerator ArrowShoot ()
	{
		while (true) {
			yield return new WaitForSeconds (startWait);

			// 1 term of ArrowShoot
			for (int i = 0; i < arrowCount; i++) {
				// Arrow Position which changes with Camera Position
				Vector3 shootPosition = new Vector3 (Camera.main.gameObject.transform.position.x + range.x, Random.Range(-range.y, range.y), range.z);
				Quaternion shootRotation = Quaternion.identity;
				// Arrow Shoot
				Instantiate (arrow, shootPosition, shootRotation);
				yield return new WaitForSeconds (shootWait);
			}
			yield return new WaitForSeconds (endWait);
		}
	}
}
