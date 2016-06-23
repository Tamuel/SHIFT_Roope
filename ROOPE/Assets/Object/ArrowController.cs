using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public GameObject arrow;
	public Vector3 range;
	public int arrowCount;

	public float startWait;
	public float spawnWait;
	public float endWait;

	void Start () 
	{
		StartCoroutine (ArrowSpawn ());
	}

	IEnumerator ArrowSpawn ()
	{
		while (true) {
			yield return new WaitForSeconds (startWait);
			for (int i = 0; i < arrowCount; i++) {
				Vector3 spawnPosition = new Vector3 (Camera.main.gameObject.transform.position.x + range.x, Random.Range(-range.y, range.y), range.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (arrow, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (endWait);
		}
	}
}
