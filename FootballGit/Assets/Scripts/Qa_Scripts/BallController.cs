/*
* GamerBox ©2018
*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
	#region Variables

	//UI Variables
	public Text countText;
	public Text goalsText;


	//Public Variables
	public AudioClip hitEffect, shootEffect;
	public Animator playerAnimator;
	public GameObject trailPrefab;
	public float minDistance = 1;
	public float speed = 3, shootAnimDuration;
	public GameObject goalText;
	public Rigidbody rg;

	//Static Variables
	public static int counter = 0;
	public static int goals = 0;

	//Private Variables
	private Transform player;
	private GameObject trail;
	private Plane playGroundPlane;
	private Vector3 startPos, endPos, direction;
	private bool inRange = true, shoot = true;
	private GameObject[] targets;
	private Transform target;
	private float upForceFactor;

	#endregion

	#region UnityFunctions

	private void Start ()
	{
		targets = new GameObject[GameObject.FindGameObjectsWithTag ("BallReciever").Length];
		for (int i = 0; i < GameObject.FindGameObjectsWithTag ("BallReciever").Length; i++) {
			targets [i] = GameObject.FindGameObjectsWithTag ("BallReciever") [i];
		}
		player = GameObject.Find ("Player").transform;
		rg = GetComponent<Rigidbody> ();
		playGroundPlane = new Plane (Vector3.up, -1);
	}

	private void FixedUpdate ()
	{
		if (Vector3.Distance (transform.position, BallReciever.BallHolder.position) > 5)
			return;
		Draw ();
	}

	private void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Goal") {
			playerAnimator.SetBool ("Dance", true);
			Invoke ("SetCameraOffset", 1.5f);
			goals++;
			countText.text = "Attempts: " + counter;
			goalsText.text = "Goals: " + goals;
			goalText.SetActive (true);
		}
	}

	private void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.tag == "3arda") {
			AudioManager.audioSource.PlayOneShot (hitEffect, 1);
		}
	}

	#endregion

	#region GamerBoxFunctions

	private void Draw ()
	{
		if (Input.touchCount > 0 || Input.GetMouseButton (0) && inRange) {
			//Touch touch = Input.GetTouch(0);
			if (/*touch.phase == TouchPhase.Began ||*/ Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float rayDistance;
				if (playGroundPlane.Raycast (ray, out rayDistance)) {
					trail = (GameObject)Instantiate (trailPrefab, ray.GetPoint (rayDistance), Quaternion.identity);
					startPos = ray.GetPoint (rayDistance);
					if (Vector3.Distance (startPos, transform.position) > minDistance) {
						inRange = false;
						return;
					}
				}
			}
			if ((/*touch.phase == TouchPhase.Moved || */Input.GetMouseButton (0)) && trail != null) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				float rayDistance;
				if (playGroundPlane.Raycast (ray, out rayDistance)) {
					trail.transform.position = ray.GetPoint (rayDistance);
				}
			}
		}

		if (/*touch.phase == TouchPhase.Ended ||*/ Input.GetMouseButtonUp (0)) {
			Destroy (trail, 2);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float rayDistance;
			if (playGroundPlane.Raycast (ray, out rayDistance)) {
				endPos = ray.GetPoint (rayDistance);
			}

			if (!inRange || startPos.z >= endPos.z || Vector3.Distance (startPos, endPos) > 200) {
				Debug.Log ("Fault");
				Destroy (trail);
				inRange = true;
				return;
			}

			target = targets [0].transform;
			float distance = Vector3.Distance (target.position, endPos);
			for (int i = 0; i < targets.Length; i++) {
				if (Vector3.Distance (target.position, endPos) < distance) {
					target = targets [i].transform;
				}
			}

			Debug.Log (target.name);
			speed = Vector3.Distance (endPos, startPos) * 15f;


			if (Vector3.Distance (target.position, transform.position) < Vector3.Distance (endPos, transform.position) || target == BallReciever.BallHolder) {
				upForceFactor = speed / 50;
				Debug.Log ("Shoot");
			} else {
				Debug.Log ("Pass");
				upForceFactor = speed / 800f;
			}
			direction = endPos - startPos;
			Debug.Log ("Play The Animation");
			playerAnimator = BallReciever.BallHolder.GetComponent<Animator> ();
			if (target.name.Contains ("Player"))
				playerAnimator.SetTrigger ("Pass");
		}
	}

	public void Pass ()
	{
		AudioManager.audioSource.PlayOneShot (shootEffect, 1);
		rg.AddForce (direction.normalized * Time.deltaTime * speed * 4 + Vector3.up * upForceFactor, ForceMode.VelocityChange);
		rg.AddTorque (Vector3.one * speed, ForceMode.VelocityChange);
		shoot = false;
	}

	private object WaitForSeconds (object shootAnim)
	{
		throw new NotImplementedException ();
	}

	#endregion
}
