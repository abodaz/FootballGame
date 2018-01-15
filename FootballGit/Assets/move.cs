using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour {

    public float speed = 3;
    Rigidbody rg;
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    public bool oneTime = true;
    public GameObject goalText;

    //for fun
    public static int counter = 0;
    public static int goals = 0;
    public Text countText;
    public Text goalsText;
    //
    // Use this for initialization
    void Start () {
        rg = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if(oneTime)
        Shoot();
       // transform.Translate(Vector3.forward*Time.deltaTime * 50);
    }


    void Shoot() {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    //for fun
                    counter++;
                    //
                    directionChosen = false;
                    break;
                    
                case TouchPhase.Moved:
                    if (oneTime) {
                        direction = touch.position - startPos;
                        directionChosen = true;
                        Invoke("endTouchMove", 0.3f);
                    }
                    break;

                case TouchPhase.Ended:
                    oneTime = false;
                    break;
            }
        }
        if (directionChosen)
        {
            // Something that uses the chosen direction...
            if (direction.y > 15 )
                direction.y = 14;

            if (direction.y < 0 )
                direction.y = 0;

            if (direction.x > 10)
                direction.x = 10;

            if (direction.x < -10)
                direction.x = -10;

            rg.AddForce(direction.x * 0.1f , direction.y * 0.1f , speed, ForceMode.Impulse);
            directionChosen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            Debug.Log("GOAL");
            //for fun
            goals ++; 
            countText.text = "Attempts: " + counter;
            goalsText.text = "Goals: " + goals;
            //
            goalText.SetActive(true);
        }
    }

    void endTouchMove() {
        oneTime = false;
    }

}
