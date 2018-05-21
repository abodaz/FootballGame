using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPurification : MonoBehaviour {

	public AudioClip oldClip;
	int[] samples;
	int channels = 8, length;
	int frequency = 128000;

	AudioSource audioSource;
	AudioClip Modifiedclip;
	void Start () 
	{	
		samples = new int[32];
		audioSource = (AudioSource) gameObject.AddComponent(typeof (AudioSource));
		Modifiedclip = AudioClip.Create("newClip", length, channels, frequency, false);
		audioSource.clip = Modifiedclip;
		audioSource.Play();
	}

	private void AnalyzeSound ()
	{
		samples = audioSource.GetOutputData(32, 0);

		//RMS
		float sum = 0;
		for (int i = 0; i < 1024; i++) {
			sum = samples [i] * samples [i];
		}
		rmsValue = Mathf.Sqrt (sum / 1024);

		//DB
		dbValue = 20 * Mathf.Log10 (rmsValue / 0.1f);

		//Spetrum using Foriuer transform. 
		audioSource.GetSpectrumData (spectrum, 0, FFTWindow.BlackmanHarris);
	}

}
