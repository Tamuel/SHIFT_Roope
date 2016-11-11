using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialView : MonoBehaviour {

    public GameObject okButton;
    public GameObject nextButton;
    public GameObject panel;
    public Image panelImage;

    public Image[] images;
    private int index;

	// Use this for initialization
	void Start () {
        index = 0;
        okButton.SetActive(false);
        panel.SetActive(false);
	}

    public void onNextButtonClicked()
    {
        if (index == images.Length - 1)
        {
            okButton.SetActive(true);
            nextButton.SetActive(false);
        }

        panelImage.sprite = images[index].sprite;

        index++;
    }

    public void onOkButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
