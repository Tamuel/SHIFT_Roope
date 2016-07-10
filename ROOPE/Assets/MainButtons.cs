using UnityEngine;
using System.Collections;

public class MainButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onRestartButtonClick()
    {
        Application.LoadLevel("Main");
    }

    public void onMainButtonClick()
    {
        Application.LoadLevel("MainMenu");
    }
}
