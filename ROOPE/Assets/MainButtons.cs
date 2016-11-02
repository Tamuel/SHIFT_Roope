using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainButtons : MonoBehaviour {
    private Object particle;
    private Image buttonImage;
    private bool pressed;
    private button pressedButton;

    private enum button { MAIN_BUTTON, RESTART_BUTTON};

    // Use this for initialization
    void Start () {
        pressed = false;
        buttonImage = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if (particle == null && pressed)
        {
            switch (pressedButton)
            {
                case button.MAIN_BUTTON:
                    SceneManager.LoadScene("MainMenu");
                    break;

                case button.RESTART_BUTTON:
                    SceneManager.LoadScene("Main");
                    break;
            }
        }
    }

    public void onRestartButtonClick()
    {
        buttonPressed();
        pressedButton = button.RESTART_BUTTON;
    }

    public void onMainButtonClick()
    {
        buttonPressed();
        pressedButton = button.MAIN_BUTTON;
    }

    public void buttonPressed()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        particle = Instantiate(Resources.Load("Prefabs/ButtonPressParticle"), transform.position, new Quaternion());
        pressed = true;
        buttonImage.enabled = false;
    }
}
