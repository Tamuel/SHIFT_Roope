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
        assignText();
    }


	void Update ()
    {

	}


    void readTextFromFile()
    {
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        
    }

    void assignText()
    {
        if (textLines[i] != null)
        {
            text.text = textLines[i];
            i++;
        }
    }

}
