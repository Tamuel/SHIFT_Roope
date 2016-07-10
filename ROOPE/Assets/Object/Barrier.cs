using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Barrier : MonoBehaviour {

    public Text gameOverText;
    public Button restartButton;
    public Button mainButton;

    void Start ()
    {
        gameOverText.text = "";
        restartButton.gameObject.SetActive(false);
        mainButton.gameObject.SetActive(false);
    }
    
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag != "Player" && other.tag != "Rope" && other.tag != "ScaleChanger") {
			Destroy (other.gameObject);
		}

        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gameOverText.text = "Game Over";
            restartButton.gameObject.SetActive(true);
            mainButton.gameObject.SetActive(true);
        }
	}
}
