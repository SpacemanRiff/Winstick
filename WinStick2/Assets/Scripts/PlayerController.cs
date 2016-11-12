using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float projectileSpeed; //this should be temporary. remove when a script for bullets is created
    public float bulletKnockback;
    public float rateOfFire;
    public GameObject gun;
    public GameObject bulletPrefab;

    private Rigidbody2D rb;
    private int timeUntilNextShot;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        timeUntilNextShot = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector3 direction = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x);
        gun.transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

        rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed, ForceMode2D.Force);

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if (Input.GetButton("Fire1") && timeUntilNextShot <= 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            bullet.transform.position = gun.transform.position + new Vector3(Mathf.Cos(angle) * 0.5f, Mathf.Sin(angle) * 0.5f, 0);
            bullet.transform.rotation = gun.transform.rotation;
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * projectileSpeed;
            Destroy(bullet, 2);

            rb.AddForce((gun.transform.right * bulletKnockback) * -1, ForceMode2D.Impulse);

            timeUntilNextShot = (int)(50.0 / rateOfFire);
        }

        if(timeUntilNextShot > 0)
        {
            timeUntilNextShot--;
        }
    }
}
