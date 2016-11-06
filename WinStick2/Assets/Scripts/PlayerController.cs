using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private Animator ac;
    private Rigidbody2D rb;
    public float speed;
    
	// Use this for initialization
	void Start () {
        ac = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x);
        rb.AddForce((transform.right*Input.GetAxis("Vertical") + transform.up*Input.GetAxis("Horizontal")).normalized * speed, ForceMode2D.Force);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Swing");
            ac.SetTrigger("Swing");
        }
    }
}
