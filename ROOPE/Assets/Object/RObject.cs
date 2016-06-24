using UnityEngine;
using System.Collections;

public abstract class RObject : MonoBehaviour, Move, Collision {

    public bool movable;
    public bool ropeAttachable;
	public bool ropeCanThrough;


	public bool isMovable() {
		return movable;
	}

	public bool isRopeAttachable() {
		return ropeAttachable;
	}

	public bool isRopeCanThrough() {
		return ropeCanThrough;
	}

	public abstract void move (float delta_x, float delta_y);
	public abstract void collideWithCharacter ();
	public abstract RopeCollisionType collideWithRopeHead ();
	public abstract RopeCollisionType collideWithRopeLine ();
}