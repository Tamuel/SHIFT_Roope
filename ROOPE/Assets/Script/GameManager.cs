using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public Text infoText;
    public Text gameOverText;
    public Text windStrengthText;
    public Button restartButton;
    public Button mainButton;

    private float hitPoint;
	private int score;
	private int numberOfRope;
	private int stage;

	private Vector2 windStrength;

	private MapController mapController;

	private Player player;


	// Make map objects
	void Awake() {
		player = FindObjectOfType<Player> ();
		hitPoint = 3.0f;
		score = 0;
		numberOfRope = 0;
		stage = 1;
		windStrength = new Vector2 (0, 0);
		mapController = GetComponent<MapController>();
	}

	// Use this for initialization
	void Start () {
        gameOverText.text = "";
        windStrengthText.text = "";
        restartButton.gameObject.SetActive(false);
        mainButton.gameObject.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
		// Show game information
		setText(ToString());

		// Potentiate player with wind
		player.wind (windStrength.x, windStrength.y);

        // If you want to potentiate RObjects with wind, remove this comment(//)
        //		foreach(RObject tempObject in FindObjectsOfType<RObject>()) {
        //			tempObject.wind (windStrength);
        //		}

        showWindStrength ();
	}

	public void setWindStrength(Vector2 windStrength) {
		this.windStrength = windStrength;
	}

	public void setWindStrength(float windStrength_x, float windStrength_y) {
		this.windStrength = new Vector2 (windStrength_x, windStrength_y);
	}

	public void setText(string str) {
		infoText.text = str;
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

    public void gameOverFunction ()
    {
        gameOverText.text = "Game Over";
        restartButton.gameObject.SetActive(true);
        mainButton.gameObject.SetActive(true);
    }

    public void showWindStrength ()
    {
        if (windStrength == new Vector2(0, 0))
            windStrengthText.text = "";
        else
            windStrengthText.text = "( x : " + windStrength.x + ", y : " + windStrength.y + " )";
    }
}