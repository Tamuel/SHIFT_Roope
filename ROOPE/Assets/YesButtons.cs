using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class YesButtons : MonoBehaviour {
    public AudioClip touchSound;

    private Object particle;
    private Image buttonImage;
    private bool pressed;
    private button pressedButton;
    public GameObject tutorialPanel;

    private enum button { YES_BUTTON, NO_BUTTON};

    // Use this for initialization
    void Start()
    {
        pressed = false;
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle == null && pressed)
        {
            switch (pressedButton)
            {
                case button.YES_BUTTON:
                    tutorialPanel.SetActive(true);
                    break;

                case button.NO_BUTTON:
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }
    }

    public void onYesButtonClick()
    {
        buttonPressed();
        pressedButton = button.YES_BUTTON;
    }

    public void onNoButtonClick()
    {
        buttonPressed();
        pressedButton = button.NO_BUTTON;
    }

    public void buttonPressed()
    {
        EffectMusicManager.instance.PlaySingle(touchSound);
        particle = Instantiate(Resources.Load("Prefabs/ButtonPressParticle"), transform.position, new Quaternion());
        pressed = true;
        buttonImage.enabled = false;
    }
}
