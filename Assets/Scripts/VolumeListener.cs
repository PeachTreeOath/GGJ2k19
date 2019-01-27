﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeListener : MonoBehaviour {

	public static float volumeLevel = 1;

	// Use this for initialization
	void Start () {
        // gameObject.GetComponent<Slider>().value = AudioManager.instance.GetMusicVolume(); 
        // VolumeChanged();
    }
	
	public void VolumeChanged()
	{
		volumeLevel = gameObject.GetComponent<Slider>().value;
		AudioManager.instance.UpdateOverallVolume();
        Debug.Log("Volume changed: " + (volumeLevel * 100f).ToString("00") + "%");
	}
}
