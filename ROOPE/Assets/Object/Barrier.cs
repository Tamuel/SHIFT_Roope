using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Barrier : MonoBehaviour {

    public Text gameOverText;

    void Start ()
    {
        gameOverText.text = "";
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
        }
	}
}
