using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	private ArrayList clouds;
	private ArrayList cloudSpeeds;
	private Camera mainCamera;
	string path;

	// Use this for initialization
	void Start () {
		path = "Prefabs/";
		mainCamera = FindObjectOfType<Camera> ();
		clouds = new ArrayList ();
		cloudSpeeds = new ArrayList ();
		StartCoroutine (MakeCloud());
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject temp in clouds) {
			int index = clouds.IndexOf (temp);
			temp.transform.position = new Vector3(temp.transform.position.x - (float)cloudSpeeds[index], temp.transform.position.y, 0);
			if (temp.transform.position.x <= mainCamera.transform.position.x - 12) {
				clouds.RemoveAt(index);
				cloudSpeeds.RemoveAt (index);
				Destroy (temp);
			}
		}
	}

	IEnumerator MakeCloud ()
	{
		while (true) {
			float waitTime = Random.Range (0.5f, 1);
			yield return new WaitForSeconds (waitTime);
			int cloudNumber = Random.Range (1, 7);
			clouds.Add(
				Instantiate (
					Resources.Load (path + "cloud" + cloudNumber),
					new Vector3(mainCamera.transform.position.x + 12f, Random.Range(-4.5f, 4.5f), 0),
					new Quaternion()
				)
			);
			float randomSize = Random.Range (0.2f, 1);
			((GameObject)clouds[clouds.Count - 1]).transform.localScale = new Vector3(randomSize, randomSize, 1);
			cloudSpeeds.Add(Random.Range(0.005f, 0.02f));
		}
	}
}
