/*
* GamerBox ©2018
*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	#region Variables

	public static CameraController Instance;
	public Vector3[] positions;
	public Vector3[] rotations;
	public float smoothSpeed = 0.2f;

	public int current = 0;

	#endregion

	#region Unity Methods

	private void Start ()
	{
		Instance = this;
	}

	private void FixedUpdate ()
	{
		transform.position = Vector3.Lerp (transform.position, positions [current], smoothSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (rotations [current]), smoothSpeed * Time.deltaTime);
	}

	public void NextTarget ()
	{
		current++;
	}

	#endregion
}


/*
 * 
 * 
 * 
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
	#region Variables

	public static CameraController Instance;
	public float smoothSpeed = 0.2f;
	public Transform target;
	public Vector3 offset;

	#endregion

	#region Unity Methods

	private void Start ()
	{
		Instance = this;
	}

	private void LateUpdate ()
	{
		transform.position = Vector3.Lerp (transform.position, target.position + CalculateOffset (BallReciever.BallHolder.position, BallReciever.nextTarget.position), Time.fixedDeltaTime * smoothSpeed);
		Debug.Log (BallReciever.nextTarget + " " + BallReciever.BallHolder);
		transform.LookAt (BallReciever.nextTarget);
	}

	public Vector3 CalculateOffset (Vector3 a, Vector3 b)
	{
		Vector3 ab = (b - a);
		ab.Normalize ();
		Vector3 calculatedOffset = a - (ab * 3);
		calculatedOffset.y = 10;
		offset = calculatedOffset;
		Debug.Log (ab);
		return calculatedOffset;
	}

	#endregion
}

*/