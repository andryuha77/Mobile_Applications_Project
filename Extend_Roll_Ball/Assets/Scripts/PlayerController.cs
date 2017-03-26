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

    //game world plane
    private Plane plane;

    //used for initialisation
    void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";

        //Create our game world plane
        //Our plane will pass through (0, 0, 0) in our game world and face up
        plane = new Plane(Vector3.up, Vector3.zero);

    }

    //input phisics location
    void FixedUpdate ()
	{
        //input select word ctrl+' for reserch in unity docummentation
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

        //     moveHorizontal = Input.gyro.userAcceleration.x;
        //     moveVertical = Input.gyro.userAcceleration.y;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);

            // adapted from http://www.blog.csimmons.catstonguesoft.com/?p=84
            //check that we have at least 1 touch on screen
            if (Input.touchCount > 0)
            {
                //grab the first touch
                Touch myTouch = Input.GetTouch(0);

                //create a ray from that touch position through the camera plane
                var ray = Camera.main.ScreenPointToRay(myTouch.position);
                float ent;

                //check if our ray will intersect the game world plane
                //if yes, return true, set ent to the DISTANCE at which the ray intersects the plane
                //if no, return false, ent is zero
                if (plane.Raycast(ray, out ent))
                {
                    //Get the point on the ray at distance 'ent'
                    var hitPoint = ray.GetPoint(ent);

                    //Add force to our rigidbody from it's position to the touch point
                    //magnitude is set to 1 with 'normalized' to keep the speed multiplier consistent
                    rb.AddForce((hitPoint - rb.position).normalized * speed);
                }

                // jumping functionaliity
                if (Input.GetKeyDown("space") && GetComponent<Rigidbody>().transform.position.y <= 0.6250001f)
                {
                    Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);

                    GetComponent<Rigidbody>().AddForce(jump);
                }

            }


            // jumping functionaliity
            if (Input.GetKeyDown("space") && GetComponent<Rigidbody>().transform.position.y <= 0.6250001f)
            {
                Vector3 jump = new Vector3(0.0f, 200.0f, 0.0f);

                GetComponent<Rigidbody>().AddForce(jump);
            }

            rb.AddForce(movement * speed);
        }
        else
        {
            float moveHorizontal = Input.gyro.userAcceleration.x;
            float moveVertical = Input.gyro.userAcceleration.y;

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);

            // adapted from http://www.blog.csimmons.catstonguesoft.com/?p=84
            //check that we have at least 1 touch on screen
            if (Input.touchCount > 0)
            {
                //grab the first touch
                Touch myTouch = Input.GetTouch(0);

                //create a ray from that touch position through the camera plane
                var ray = Camera.main.ScreenPointToRay(myTouch.position);
                float ent;

                //check if our ray will intersect the game world plane
                //if yes, return true, set ent to the DISTANCE at which the ray intersects the plane
                //if no, return false, ent is zero
                if (plane.Raycast(ray, out ent))
                {
                    //Get the point on the ray at distance 'ent'
                    var hitPoint = ray.GetPoint(ent);

                    //Add force to our rigidbody from it's position to the touch point
                    //magnitude is set to 1 with 'normalized' to keep the speed multiplier consistent
                    rb.AddForce((hitPoint - rb.position).normalized * speed);
                }
            }
        }
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
		if (count >= 17)
		{
			winText.text = "You Win!";
		}
	}
}

