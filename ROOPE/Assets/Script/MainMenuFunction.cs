using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
				StartCoroutine (ChangeLevel ());
				break;

			case button.SCORE_BUTTON:
				//Application.LoadLevel ("Score");
				break;

			case button.EXIT_BUTTON:
				Application.Quit ();
				break;
			}
		}
	}

	IEnumerator ChangeLevel () {
		float fadeTime = GameObject.Find ("Canvas").GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
        // initialize
        if (PlayerPrefs.GetString("Tutorial", "Default") == "Default")
        {
            PlayerPrefs.SetString("Tutorial", "Yes");
        }

        if (PlayerPrefs.GetString("Tutorial") == "Yes")
        {
            SceneManager.LoadScene("TutorialMenu");
        }
        else if (PlayerPrefs.GetString("Tutorial", "No") == "No")
        {
            SceneManager.LoadScene("Main");
        }
	}

	public void buttonPressed() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
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
