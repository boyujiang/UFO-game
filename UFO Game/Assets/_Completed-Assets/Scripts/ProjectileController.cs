using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float velocity = 3f;
    private float lifespan = 3f;
    private float startTime = 0f;
    public int playerdamage;
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized * velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + lifespan)
        {
            Destroy(gameObject);
        }
        //rb2d.velocity = new Vector2(velocityX, velocityY); 
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(collision.gameObject);
        if (!other.gameObject.CompareTag("PickUp"))
        {
            Destroy(gameObject);
        }
    }
}
