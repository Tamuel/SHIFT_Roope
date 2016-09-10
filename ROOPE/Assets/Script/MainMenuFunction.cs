using UnityEngine;
using System.Collections;

public class MainMenuFunction : MonoBehaviour {
	private bool up;
	private float range = 0.3f;
	private float speed;
	private Vector3 startPosition;
	private ParticleSystem particle;

	void Update() {
		if(particle && !particle.IsAlive() == false)
			Application.LoadLevel("Main");
	}

	public void onStartButtonClick() {
		particle = Instantiate (Resources.Load ("Prefabs/ButtonPressParticle"), transform.position, new Quaternion()) as ParticleSystem;
	}

	public void onScoreButtonClick() {
		
	}

	public void onExitButtonClick() {
		Application.Quit ();
	}
}
