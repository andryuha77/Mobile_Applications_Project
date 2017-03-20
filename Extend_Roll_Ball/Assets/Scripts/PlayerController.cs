using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	//reference varible
	private Rigidbody rb;
	private int count;

	//used for initialisation
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
	}

	//input phisics location
	void FixedUpdate ()
	{
		//input select word ctrl+' for reserch in unity docummentation
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		GetComponent<Rigidbody>().AddForce (movement * speed * Time.deltaTime);
	// jumping functionaliity
		{
			if (Input.GetKeyDown ("space") && GetComponent<Rigidbody>().transform.position.y <= 0.6250001f) {
				Vector3 jump = new Vector3 (0.0f, 200.0f, 0.0f);

				GetComponent<Rigidbody>().AddForce (jump);
			}
		}

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			//activate or deactivate gameobject
			other.gameObject.SetActive (false);
			//increment cont
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 10)
		{
			winText.text = "You Win!";
		}
	}
}