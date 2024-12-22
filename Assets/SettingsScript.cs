using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour{

	public AudioMixer audioMixer;

	public TMP_Dropdown resolutionDropdown;

	Resolution[] resolutions;

void Start()
{
    resolutions = Screen.resolutions;

    List<Resolution> filteredResolutions = new List<Resolution>();
    List<string> options = new List<string>();

    int currentResolutionIndex = 0;

    for (int i = 0; i < resolutions.Length; i++)
    {
        if (resolutions[i].width >= 1280 && resolutions[i].width % 320 == 0)
        {
            filteredResolutions.Add(resolutions[i]);

            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = filteredResolutions.Count - 1;
            }
        }
    }

    resolutions = filteredResolutions.ToArray();

    resolutionDropdown.ClearOptions();
    resolutionDropdown.AddOptions(options);
    resolutionDropdown.value = currentResolutionIndex;
    resolutionDropdown.RefreshShownValue();
}

public void SetResolution(int resolutionIndex)
{
    Resolution resolution = resolutions[resolutionIndex];
    Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
}

	public void SetVolume (float volume)
	{
        float minVolume = -20f;
        float maxVolume = 6f;

        float adjustedVolume = Mathf.Lerp(minVolume, maxVolume, Mathf.Pow((volume + 20f) / 26f, 2f));

		audioMixer.SetFloat("volume", volume);
        if (volume <= -20f)
        {
            audioMixer.SetFloat("volume", -80f);
        }
	}

	public void SetQuality (int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

	public void SetFullscreen (bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}

}