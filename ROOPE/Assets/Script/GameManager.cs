using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {
	private string FILE_PATH = Application.persistentDataPath + "/" + "map.dat";
	private Hashtable MapObjects;

	private float hitPoint = 3.0f;
	private int score = 0;
	private int numberOfRope = 20;
	private int stage = 1;


	// Make map objects
	void Awake() {
		MapObjects = new Hashtable ();
		readMapFromFile ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}


	void WriteFile()
	{
		StreamWriter fileWriter = new StreamWriter (FILE_PATH);

		// write file
		fileWriter.WriteLine ("Hello world");
		fileWriter.Flush ();
		fileWriter.Close ();
	}


	void readMapFromFile()
	{
		StreamReader fileReader = new StreamReader (FILE_PATH);

		// read file
		while(!fileReader.EndOfStream)
		{
			string line = fileReader.ReadLine ();
		}

		fileReader.Close ();
	}

	public void nextStage() {
		stage++;
	}

	public float getHP() {
		return hitPoint;
	}

	public void setHP(float hitPoint) {
		this.hitPoint = hitPoint;
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public void addScroe(int score) {
		setScore (getScore () + score);
	}

	public int getNumberOfRope() {
		return numberOfRope;
	}

	public void setNumberOfRope(int numberOfRope) {
		this.numberOfRope = numberOfRope;
	}

	public int getStage() {
		return stage;
	}
}