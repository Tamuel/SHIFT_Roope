using UnityEngine;
using System.Collections;

public class WindControl : MonoBehaviour,Collision {

	public float x_Strength;
	public float y_Strength;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Action when collide with character
	public void collideWithCharacter(Player player) {
		FindObjectOfType<GameManager> ().setWindStrength (x_Strength, y_Strength);
		Destroy (this);
	}

	// Action when collide with rope head
	public RopeCollisionType collideWithRopeHead(Rope rope) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}

	// Action when collide with rope line
	public RopeCollisionType collideWithRopeLine(RopeLine line) {
		return RopeCollisionType.CAN_NOT_ATTACH_AND_THROUGH;
	}
		
}
