using UnityEngine;
using System.Collections;

public class MainMenuFunction : MonoBehaviour {
	private bool up;
	private float range = 0.3f;
	private float speed;
	private Vector3 startPosition;

	public void onStartButtonClick() {
		Application.LoadLevel("Main");
	}

	public void onScoreButtonClick() {
		
	}

	public void onExitButtonClick() {
		Application.Quit ();
	}
}
