using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour {

		public void VolumeControl(float volumeControl) {
		AudioListener.volume = volumeControl; 
	}
}
