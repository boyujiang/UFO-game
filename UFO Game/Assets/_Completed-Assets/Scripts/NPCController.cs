using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private float latestDirectionChangeTime;
    private float directionChangeTime = 100f;
    protected float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private Rigidbody2D rb2d;
    private float spriteHeight = 256;
    private float spriteWidth = 256;
    private float width = 400;
    private float height = 400;


    public void Start()
    {
        latestDirectionChangeTime = 0f;
        rb2d = GetComponent<Rigidbody2D>();
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }



    public void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time > directionChangeTime * Random.value + latestDirectionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        //transform.position = new Vector2(Mathf.Min(transform.position.x + (movementPerSecond.x * Time.deltaTime), width - spriteWidth),
        //Mathf.Min(transform.position.y + (movementPerSecond.y * Time.deltaTime), height - spriteHeight));
        rb2d.velocity = movementPerSecond;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
}