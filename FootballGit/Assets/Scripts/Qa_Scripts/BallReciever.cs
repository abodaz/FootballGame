/*
* GamerBox ©2018
*/
using UnityEngine;

public class BallReciever : MonoBehaviour
{
	public static Transform endPosOfLine;
	public static Transform BallHolder;
	public static Transform nextTarget;

	public static BallReciever Instance;

	public Transform recieverPos, shooterPos;
	private BallController ballCont;
	private GameObject[] recievers;

	private void Start ()
	{
		Instance = this;
		recievers = GameObject.FindGameObjectsWithTag ("BallReciever");
		ballCont = GameObject.FindObjectOfType<BallController> ().GetComponent <BallController> ();
		FindTheBallHolderAndTheNextTarget ();
	}


	private void Update ()
	{
		if (Vector3.Distance (ballCont.transform.position, nextTarget.transform.position) < 2 && BallHolder != nextTarget) {
			CancelInvoke ();
			ballCont.rg.velocity = Vector3.zero;
			BallReciever.nextTarget.GetComponent <Animator> ().SetTrigger ("Recieve");
			ballCont.transform.position = nextTarget.GetComponent<BallReciever> ().shooterPos.position;
			FindTheBallHolderAndTheNextTarget ();
			CameraController.Instance.NextTarget ();
		}
	}

	public void ReloadLevel ()
	{
		Invoke ("ReloadLevelNow", 6);
	}

	void ReloadLevelNow ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex);
	}

	public void Shoot ()
	{
		ballCont.Pass ();
	}

	private void FindTheBallHolderAndTheNextTarget ()
	{
		//Find The Nearest Player To The Ball.
		BallHolder = recievers [0].transform;
		foreach (GameObject reciever in recievers) {
			if (Vector3.Distance (reciever.transform.position, ballCont.transform.position) < Vector3.Distance (BallHolder.position, ballCont.transform.position)) {
				BallHolder = reciever.transform;
			}
		}

		//Find The Nearest Target To The Ball Holder
		nextTarget = recievers [0].transform;
		foreach (GameObject reciever in recievers) {
			if (Vector3.Distance (BallHolder.position, reciever.transform.position) < Vector3.Distance (BallHolder.position, nextTarget.position)
			    && reciever.transform != BallHolder && reciever.transform.position.z > BallHolder.position.z) {
				nextTarget = reciever.transform;
			}
		}
	}
}
