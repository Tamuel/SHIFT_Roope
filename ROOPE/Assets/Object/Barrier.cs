using UnityEngine;
using System.Collections;

public class Barrier : MonoBehaviour {
    
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag != "Player" && other.tag != "Rope" && other.tag != "ScaleChanger" && other.tag != "Barrier") {
			Destroy (other.gameObject);
		}

        if (other.tag == "Player")
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            FindObjectOfType<GameManager>().gameOverFunction();
        }
	}
}
