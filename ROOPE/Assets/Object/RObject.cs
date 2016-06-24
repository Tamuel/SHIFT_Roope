using UnityEngine;
using System.Collections;

public class RObject : MonoBehaviour {

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

}