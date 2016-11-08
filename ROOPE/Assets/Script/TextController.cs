using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public TextAsset textFile;
	public string[] textLines;
    public Text text;
    private int i = 0;

	void Start ()
    {
        readTextFromFile();
		Debug.Log ("Num of Lines : " + textLines.Length);
		assignText();
    }


	void Update ()
    {

	}


    private void readTextFromFile()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        
    }

    public void assignText()
    {
		//if (i < textLines.Length)
  //      {
  //          text.text = textLines[i];
  //          i++;
  //      }
    }

}
