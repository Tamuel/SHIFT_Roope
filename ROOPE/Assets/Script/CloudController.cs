using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudController : MonoBehaviour {
    public GameObject[] cloudType;

	private List<GameObject> clouds;
	private ArrayList cloudSpeeds;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = FindObjectOfType<Camera> ();
		clouds = new List<GameObject>();
		cloudSpeeds = new ArrayList ();
		StartCoroutine (MakeCloud());
	}
	
	// Update is called once per frame
	void Update () {
        listErrorCheck();

        for (int i = 0; i < clouds.Count; i++)
        {
            Vector3 temp = clouds[i].transform.position;
            temp = new Vector3(temp.x - (float)cloudSpeeds[i], temp.y, 0);
            clouds[i].transform.position = temp;

            if (temp.x <= mainCamera.transform.position.x - 40)
            {
                clouds.RemoveAt(i);
                cloudSpeeds.RemoveAt(i);
                Destroy(clouds[i]);
                i--;
            }
        }
	}

    private void listErrorCheck()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            if (!clouds[i])
            {
                clouds.RemoveAt(i);
                cloudSpeeds.RemoveAt(i);
                i--;
            }
        }
    }

	IEnumerator MakeCloud ()
	{
		while (true) {
			float waitTime = Random.Range (0.5f, 1);
			yield return new WaitForSeconds (waitTime);
			int cloudNumber = Random.Range (0, cloudType.Length);
			clouds.Add(
				(GameObject)Instantiate (
					cloudType[cloudNumber],
					new Vector3(mainCamera.transform.position.x + 12f, Random.Range(-4.5f, 4.5f), 0),
					Quaternion.identity
				)
			);
			float randomSize = Random.Range (0.2f, 1);
			(clouds[clouds.Count - 1]).transform.localScale = new Vector3(randomSize, randomSize, 1);
			cloudSpeeds.Add(Random.Range(0.005f, 0.02f));
		}
	}
}
