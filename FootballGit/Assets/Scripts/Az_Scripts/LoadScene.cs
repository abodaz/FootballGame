using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	// public float interval;
	// if(interval > 0)
	// 		{
	// 				interval -= Time.deltaTime;					
	// 		}
	// 		else
	// 		{
	// 			
	// 		}
		public void ChangeToScene (string sceneName)
		{		
			LoadingScreen.LoadScene(sceneName);
		}
}
