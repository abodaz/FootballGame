using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public static AudioSource audioSource;

	private void Awake()
	{
		if (audioSource != null || SceneManager.GetActiveScene().buildIndex != 1)
		{
			Destroy(this.gameObject);
		}
		else
		{
			DontDestroyOnLoad(this.gameObject);
			audioSource = GetComponent<AudioSource>();
		}
	}
}
