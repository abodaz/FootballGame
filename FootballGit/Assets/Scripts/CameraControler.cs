using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour {

    public GameObject ballPref;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - ballPref.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = ballPref.transform.position + offset;
	}
}
