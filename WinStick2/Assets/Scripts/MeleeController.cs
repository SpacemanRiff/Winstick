using UnityEngine;
using System.Collections;

public class MeleeController : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Physics2D.IgnoreCollision(GameObject.Find("Tile2").GetComponent<Collider2D>(), GetComponent<Collider2D>());
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}
}
