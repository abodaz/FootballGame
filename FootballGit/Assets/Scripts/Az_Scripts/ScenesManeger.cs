using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesManeger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Reloadlvl()
    {
        SceneManager.LoadScene("1"); 
    }
}
