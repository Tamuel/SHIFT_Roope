using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, Wind
{

    HingeJoint2D[] hingeJoint2D;

    public GameObject rope;

    public GameObject rope1Prefab;
    public GameObject rope2Prefab;

    private float relativeVectorFromTouchPointToPlayerX;
    private float relativeVectorFromTouchPointToPlayerY;

    private const float maxSpeed = 10;

    void Start()
    {
        hingeJoint2D = GetComponents<HingeJoint2D>();

        rope1Prefab = Instantiate(rope);
        rope1Prefab.transform.parent = transform;
        rope1Prefab.transform.localScale = new Vector3(4, 4, 4);
		rope1Prefab.GetComponent<Rope> ().setPlayer(this);
		rope1Prefab.GetComponent<Rope> ().hingeJoint2D = hingeJoint2D [0];

        Debug.Log(
            rope1Prefab.transform.position.ToString() + "\n" +
            rope1Prefab.transform.rotation.ToString() + "\n" +
            rope1Prefab.transform.localScale.ToString()
        );

        rope2Prefab = Instantiate(rope);
        rope2Prefab.transform.parent = transform;
        rope2Prefab.transform.localScale = new Vector3(4, 4, 4);
		rope2Prefab.GetComponent<Rope> ().setPlayer(this);
		rope2Prefab.GetComponent<Rope> ().hingeJoint2D = hingeJoint2D [1];

        Debug.Log(rope2Prefab.transform.position.ToString());
    }

    void FixedUpdate()
    {

        if (rope1Prefab.GetComponent<Rope>().isRopeAttached && rope2Prefab.GetComponent<Rope>().isRopeAttached)
            GetComponent<Rigidbody2D>().gravityScale = 0;
        else
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;
    }

    void Update()
    {

        // Shoot Rope
        if (Input.touchCount >= 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
				if (shootRope (rope1Prefab, Input.GetTouch (0).position)) {
				} else shootRope(rope2Prefab, Input.GetTouch(0).position);
            }
            if (Input.GetTouch(1).phase == TouchPhase.Began)
			{
				if (shootRope (rope1Prefab, Input.GetTouch (1).position)) {
				} else shootRope(rope2Prefab, Input.GetTouch(1).position);
            }
        }


        // Stop Rope
        if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended &&
            rope1Prefab.GetComponent<Rope>().isRopeLaunched)
            stopRope(rope1Prefab);
        if (Input.touchCount != 0 && Input.GetTouch(1).phase == TouchPhase.Ended &&
            rope2Prefab.GetComponent<Rope>().isRopeLaunched)
            stopRope(rope2Prefab);
        if (Input.touchCount == 0)
        {
            stopRope(rope1Prefab);
            stopRope(rope2Prefab);
        }
    }

    void stopRope(GameObject rope)
    {
        rope.GetComponent<Rope>().stopRope();
    }

    bool shootRope(GameObject rope, Vector3 touchPosition)
    {
        return rope.GetComponent<Rope>().launchRope(touchPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<RObject>() != null)
            other.GetComponent<RObject>().collideWithCharacter(this);
    }

    // Wind blow with force
    public void wind(float force_x, float force_y)
    {
        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.AddForce(new Vector2(force_x, force_y));

        // Clamp Speed
        if (rigidBody2D.velocity.magnitude > maxSpeed)
        {
            float bias = maxSpeed / rigidBody2D.velocity.magnitude;
            rigidBody2D.velocity = new Vector2(
                rigidBody2D.velocity.x * bias,
                rigidBody2D.velocity.y * bias
            );
        }
    }
}