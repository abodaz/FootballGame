using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour 
{
	#region Variables
	public static CameraController Instance;
	public Transform target;
	public float smoothSpeed = 0.2f;
	public Vector3 offset;
	#endregion

	#region Unity Methods
	private void Start()
	{
		Instance = this;
	}
	private void FixedUpdate()
	{
		if (target == null)
			return;
		Vector3 newPos = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed * Time.deltaTime);
		transform.position = newPos;
		transform.LookAt(target);
	}
	#endregion
}
