using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
	public TMP_Dropdown resolutionDropdown;
	Resolution[] _resolutions;
	void Start()
    {
		resolutionDropdown.ClearOptions();
		List<string> options = new List<string>();
		_resolutions = Screen.resolutions;
		
		int currentResolutionIndex = 0;
		for (int i = 0; i < _resolutions.Length; i++)
		{
			string option = _resolutions[i].width + " x " + _resolutions[i].height;
			options.Add(option);

			if (_resolutions[i].width == Screen.currentResolution.width &&
				_resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i;
			}
		}
		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
		Debug.Log("uspeo");
		gameObject.SetActive(false);
	}

	public void SetResolution(int resolutionIndex)
	{
		Screen.SetResolution(_resolutions[resolutionIndex].width, _resolutions[resolutionIndex].height, Screen.fullScreen);
	}

	public void SetFullscreen(bool state)
	{
		Screen.fullScreen = state;
	}

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
	}
}
