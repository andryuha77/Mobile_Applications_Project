using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	//reference varible
	private Rigidbody rb;

	//used for initialisation
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	//input phisics location
	void FixedUpdate ()
	{
		//input select word ctrl+' for reserch in unity docummentation
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}
}