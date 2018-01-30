/*
* GamerBox ©2018
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
	#region Variables

	//UI Variables
	public Text countText;
	public Text goalsText;


	//Public Variables
	public Animator playerAnimator;
	public GameObject trailPrefab;
	public float minDistance = 1;
	public float speed = 3, shootAnimDuration;
	public GameObject goalText;

	//Static Variables
	public static int counter = 0;
	public static int goals = 0;

	//Private Variables
	private GameObject trail;
	private Rigidbody rg, trailRb;
	private Plane playGroundPlane;
	private Vector3 startPos, endPos, direction;
	private bool inRange = true, shoot = true;
	#endregion

	#region UnityFunctions
	private void Start()
	{
		rg = GetComponent<Rigidbody>();
		playGroundPlane = new Plane(Vector3.up, -1);
	}

	private void FixedUpdate()
	{
		if (!shoot)
			return;
		Draw();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Goal")
		{
			goals++;
			countText.text = "Attempts: " + counter;
			goalsText.text = "Goals: " + goals;
			goalText.SetActive(true);
		}
	}
	#endregion

	#region GamerBoxFunctions
	private void Draw()
	{
		if (inRange)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float rayDistance;
				if (playGroundPlane.Raycast(ray, out rayDistance))
				{
					trail = (GameObject)Instantiate(trailPrefab, ray.GetPoint(rayDistance), Quaternion.identity);
					trailRb = trail.GetComponent<Rigidbody>();
					startPos = ray.GetPoint(rayDistance);
					if (Vector3.Distance(startPos, transform.position) > minDistance)
					{
						inRange = false;
					}
				}
			}
			else if (Input.GetMouseButton(0) && trail != null)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float rayDistance;
				if (playGroundPlane.Raycast(ray, out rayDistance))
				{
					trail.transform.position = ray.GetPoint(rayDistance);
				}
			}
			else if (Input.GetMouseButtonUp(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float rayDistance;
				if (playGroundPlane.Raycast(ray, out rayDistance))
				{
					endPos = ray.GetPoint(rayDistance);
				}
				speed = Vector3.Distance(endPos, startPos) * 30f;
				direction = endPos - startPos;
				playerAnimator.SetTrigger("Shoot");
				Invoke("Shoot", shootAnimDuration);
			}
		}
		if (Input.GetMouseButtonUp(0))
		{
			inRange = true;
			Destroy(trail, 3);
		}
	}

	private void Shoot()
	{
		rg.AddForce(direction.normalized * Time.deltaTime * speed * 2 + Vector3.up * speed / 80f, ForceMode.VelocityChange);
		rg.AddTorque(Vector3.one * speed, ForceMode.VelocityChange);
		shoot = false;
	}
	private object WaitForSeconds(object shootAnim)
	{
		throw new NotImplementedException();
	}
	#endregion
}
