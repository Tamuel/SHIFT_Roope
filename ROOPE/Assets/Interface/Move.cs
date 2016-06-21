using UnityEngine;
using System.Collections;

public interface Move {

    // Move by delta_x in x coordinate and delta_y in y coordinate
    void move(float delta_x, float delta_y);
}
