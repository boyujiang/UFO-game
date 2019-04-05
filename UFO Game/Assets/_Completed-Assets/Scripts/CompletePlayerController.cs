using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
    public Text healthText;
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
    public Text timer;

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
    private int health;
    private float timeLeft;
    private float startTime;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		count = 0;
        health = 100;
        timeLeft = 120f;
        startTime = Time.time;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";
        timer.text = "Time left: " + timeLeft;

        //Call our SetCountText function which will update the text with the current value for count.
        SetCountText ();
        SetHealthText();
	}

    ////FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    //void FixedUpdate()
    //{
    //	//Store the current horizontal input in the float moveHorizontal.
    //	float moveHorizontal = Input.GetAxis ("Horizontal");

    //	//Store the current vertical input in the float moveVertical.
    //	float moveVertical = Input.GetAxis ("Vertical");

    //	//Use the two store floats to create a new Vector2 variable movement.
    //	Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

    //	//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
    //	rb2d.AddForce (movement * speed);
    //}
    void FixedUpdate()
    {
        float movehorizontal = Input.GetAxis("Horizontal");
        float movevertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(movehorizontal, movevertical);
        rb2d.MovePosition(rb2d.position + movement * speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            timer.text = "Time left: " + ((int)timeLeft).ToString();
            winText.text = "Time up";
            Destroy(gameObject);
        }
        else
        {
            timer.text = "Time left: " + ((int)timeLeft).ToString();
        }

    }

    void OnCollisionEnter2D(Collision2D other) 
	{
        //TODO: Make the player able to walk on water
        if (other.gameObject.tag == "water")
        {
            Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
        }
           
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.tag == "PickUp") 
		{
            //... then set the other object we just collided with to inactive.
            Destroy(other.gameObject);
			
			//Add one to the current value of our count variable.
			count = count + 1;
			
			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}

        if (other.gameObject.CompareTag("Projectile"))
        {
            //... then set the other object we just collided with to inactive.
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            health = health - 10;

            //Update the currently displayed count by calling the SetCountText function.
            SetHealthText();
        }


    }

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12)
			//... then set the text property of our winText object to "You win!"
			winText.text = "You win!";
	}

    //This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
    void SetHealthText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        healthText.text = "Health: " + health.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (health <= 0)
            //... then set the text property of our winText object to "You win!"
            winText.text = "You lose";
    }
}
