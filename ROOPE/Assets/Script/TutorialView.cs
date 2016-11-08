using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialView : MonoBehaviour {

    public GameObject seeAgainButton;
    public GameObject okButton;
    public GameObject nextButton;
    public GameObject panel;
    public Image panelImage;

    public Image[] images;
    private int index;

	// Use this for initialization
	void Start () {
        index = 0;
        seeAgainButton.SetActive(false);
        okButton.SetActive(false);
        panel.SetActive(false);
	}

    public void onNextButtonClicked()
    {
        if (index == images.Length - 1)
        {
            seeAgainButton.SetActive(true);
            okButton.SetActive(true);
        }

        panelImage.sprite = images[index].sprite;

        index++;
    }

    public void onSeeAgainButtonClicked()
    {
        PlayerPrefs.SetString("Tutorial", "No");
        SceneManager.LoadScene("Main");
    }

    public void onOkButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
