using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour {

	private ParticleSystem particleSystem;

	// Use this for initialization
	void Start () {
		particleSystem = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(particleSystem && !particleSystem.IsAlive ())
				Destroy (gameObject);
	}
}
