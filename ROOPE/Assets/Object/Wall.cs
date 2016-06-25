﻿using UnityEngine;
using System.Collections;

public class Wall : Obstacle {

	private bool canDrop;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player") {

			collideWithCharacter ();
		}
	}

	public override void collideWithCharacter ()
	{
		FindObjectOfType<GameManager> ().addHP (-1);
	}

	public override RopeCollisionType collideWithRopeHead(Rope rope){//Rope rope) {
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}


	public override RopeCollisionType collideWithRopeLine (RopeLine line){//Line line) {
		if (isRopeAttachable ())
			return RopeCollisionType.CAN_ATTACH;
		else
			return RopeCollisionType.CAN_NOT_ATTACH_AND_CUT;
	}
}
