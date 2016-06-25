using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text infoText;

	private float hitPoint;
	private int score;
	private int numberOfRope;
	private int stage;


	// Make map objects
	void Awake() {
		hitPoint = 3.0f;
		score = 0;
		numberOfRope = 0;
		stage = 1;
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		infoText.text = ToString();
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

	public void addHP(float hp) {
		setHP (getHP () + hp);
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public void addScore(int score) {
		setScore (getScore () + score);
	}

	public int getNumberOfRope() {
		return numberOfRope;
	}

	public void setNumberOfRope(int numberOfRope) {
		this.numberOfRope = numberOfRope;
	}

	public void addNumberOfRope(int numberOfRope) {
		setNumberOfRope(getNumberOfRope() + numberOfRope);
	}

	public int getStage() {
		return stage;
	}

	public override string ToString() {
		return "STAGE : " + getStage () + "  HP : " + getHP () +
		"  SCORE : " + getScore () + "  ROPE : " + getNumberOfRope ();
	}
}