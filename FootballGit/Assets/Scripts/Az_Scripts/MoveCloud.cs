using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -2521)
            transform.position = new Vector3(transform.position.x, 3000, transform.position.z);
        if(transform.rotation.eulerAngles.z == 180)
            transform.Translate(-Vector3.up*-1 * Time.deltaTime * speed);
        else
            transform.Translate(-Vector3.up * Time.deltaTime * speed);
    }
}
