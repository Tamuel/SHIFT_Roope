using UnityEngine;
using System.Collections;

public interface Collision {

    // Action when collide with character
    void collideWithCharacter();

	// Action when collide with rope head
	RopeCollisionType collideWithRopeHead();//Rope rope);

	// Action when collide with rope line
	RopeCollisionType collideWithRopeLine();//Line line);

}