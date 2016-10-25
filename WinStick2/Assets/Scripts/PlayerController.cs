using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Rigidbody2D rb;
    public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed), ForceMode2D.Force);
        transform.rotation = Quaternion.LookRotation(new Vector2(rb.velocity.x,rb.velocity.y));
        Debug.Log(Vector2.Angle(Vector2.right, new Vector2(rb.velocity.x, rb.velocity.y)));
	}
}
