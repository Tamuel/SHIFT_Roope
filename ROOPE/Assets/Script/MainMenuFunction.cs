using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuFunction : MonoBehaviour {
	private bool pressed;
	private enum button {START_BUTTON, SCORE_BUTTON, EXIT_BUTTON};
	private button pressedButton;
	private Object particle;

	private Image buttonImage;

	void Start() {
		pressed = false;
		buttonImage = GetComponent<Image> ();
	}

	void Update() {
		if(particle == null && pressed) {
			switch(pressedButton) {
			case button.START_BUTTON:
				Application.LoadLevel ("Main");
				break;

			case button.SCORE_BUTTON:
				//Application.LoadLevel ("Main");
				break;

			case button.EXIT_BUTTON:
				Application.Quit ();
				break;
			}
		}
	}

	public void buttonPressed() {
		particle = Instantiate (Resources.Load ("Prefabs/ButtonPressParticle"), transform.position, new Quaternion());
		pressed = true;
		buttonImage.enabled = false;

	}

	public void onStartButtonClick() {
		buttonPressed ();
		pressedButton = button.START_BUTTON;
	}

	public void onScoreButtonClick() {
		buttonPressed ();
		pressedButton = button.SCORE_BUTTON;
	}

	public void onExitButtonClick() {
		buttonPressed ();
		pressedButton = button.EXIT_BUTTON;
	}
}
