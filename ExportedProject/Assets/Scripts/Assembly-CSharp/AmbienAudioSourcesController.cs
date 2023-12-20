using UnityEngine;

public class AmbienAudioSourcesController : MonoBehaviour
{
	private float soundsVolume;

	public AudioSource rocks;

	public AudioSource trees;

	public AudioSource animals;

	public AudioSource sea;

	public AudioSource boat;

	public AudioSource castle;

	public AudioSource tower;

	public AudioSource space;

	public AudioSource field;

	private AudioSource[] audioSources;

	private void Start()
	{
		soundsVolume = GlobalSettings.globalSettings.soundsVolume;
		audioSources = new AudioSource[9] { rocks, trees, animals, sea, boat, castle, tower, space, field };
		UpdateAudioSources();
	}

	private void Update()
	{
		if (soundsVolume != GlobalSettings.globalSettings.soundsVolume)
		{
			soundsVolume = GlobalSettings.globalSettings.soundsVolume;
			UpdateAudioSources();
		}
	}

	private void UpdateAudioSources()
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			audioSources[i].volume = soundsVolume;
		}
	}
}
