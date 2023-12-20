using UnityEngine;

public class GlobalAudio : MonoBehaviour
{
	public static GlobalAudio globalAudio;

	private AudioSource[] audioSources;

	private float soundVolume;

	private float musicVolume;

	private float narrationVolume;

	private float[] originalVolumes;

	private string[] audioTypes;

	private bool[] replaces;

	private float[] replacesVolume;

	private float[] replacesPitch;

	private float[] replacesPan;

	private float panMax = 0.9f;

	public bool fadeMusic;

	private bool[] pauseRecord;

	private void Start()
	{
		if (globalAudio == null)
		{
			globalAudio = this;
		}
		else
		{
			Object.Destroy(this);
		}
		Initialize();
	}

	private void Update()
	{
		UpdateVolume();
		CheckConditions();
	}

	public void Initialize()
	{
		InitializeAudioSources();
		InitializeOriginalVolumes();
		InitializeAudioTypes();
		InitializeReplaces();
		pauseRecord = new bool[audioSources.Length];
	}

	public void PauseAll()
	{
		pauseRecord = new bool[audioSources.Length];
		for (int i = 0; i < pauseRecord.Length; i++)
		{
			if (audioSources[i].isPlaying)
			{
				audioSources[i].Pause();
				pauseRecord[i] = true;
			}
			else
			{
				pauseRecord[i] = false;
			}
		}
	}

	public void UnpauseAll()
	{
		for (int i = 0; i < pauseRecord.Length; i++)
		{
			if (pauseRecord[i])
			{
				audioSources[i].Play();
			}
		}
	}

	public bool IsAnythingPlaying()
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (audioSources[i].isPlaying)
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateVolume()
	{
		if (soundVolume == GlobalSettings.globalSettings.soundsVolume && musicVolume == GlobalSettings.globalSettings.musicVolume && narrationVolume == GlobalSettings.globalSettings.narrationVolume)
		{
			return;
		}
		soundVolume = GlobalSettings.globalSettings.soundsVolume;
		musicVolume = GlobalSettings.globalSettings.musicVolume;
		narrationVolume = GlobalSettings.globalSettings.narrationVolume;
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (audioTypes[i] == "sound")
			{
				audioSources[i].volume = originalVolumes[i] * soundVolume;
			}
			else if (audioTypes[i] == "music")
			{
				audioSources[i].volume = originalVolumes[i] * musicVolume;
			}
			else if (audioTypes[i] == "narration")
			{
				audioSources[i].volume = originalVolumes[i] * narrationVolume;
			}
		}
	}

	private void CheckConditions()
	{
		for (int i = 2; i < audioSources.Length; i++)
		{
			if (audioSources[i].isPlaying)
			{
				if (replaces[i])
				{
					Replace(i);
				}
				else if ((float)audioSources[i].timeSamples > (float)audioSources[i].clip.samples * 0.9f)
				{
					ReduceVol(i);
				}
			}
			else
			{
				replaces[i] = false;
			}
		}
		if (fadeMusic)
		{
			FadeMusic();
		}
	}

	private void ReduceVol(int i)
	{
		float volume = audioSources[i].volume;
		float num = originalVolumes[i] / 4f;
		audioSources[i].volume -= num;
		if (audioSources[i].volume < 0.01f && volume > 0.01f)
		{
			audioSources[i].volume = 0.01f;
		}
		else if (volume == 0.01f)
		{
			audioSources[i].volume = 0f;
			audioSources[i].Stop();
		}
	}

	private void FadeMusic()
	{
		float volume = audioSources[1].volume;
		float num = originalVolumes[1] / 240f;
		audioSources[1].volume -= num;
		if (audioSources[1].volume < 0.01f && volume > 0.01f)
		{
			audioSources[1].volume = 0.01f;
		}
		else if (volume == 0.01f)
		{
			audioSources[1].volume = 0f;
		}
		if (audioSources[1].volume == 0f)
		{
			fadeMusic = false;
			audioSources[1].Stop();
		}
	}

	public void ActivateFadeMusic()
	{
		if (audioSources[1].volume > 0f)
		{
			fadeMusic = true;
		}
		else
		{
			audioSources[1].Stop();
		}
	}

	private void Replace(int i)
	{
		ReduceVol(i);
		if (audioSources[i].volume == 0f)
		{
			audioSources[i].volume = replacesVolume[i];
			audioSources[i].pitch = replacesPitch[i];
			audioSources[i].panStereo = replacesPan[i];
			audioSources[i].Play();
			replaces[i] = false;
		}
	}

	public AudioSource PlaySound(AudioClip clip, float vol, float pitch, float pan)
	{
		int availableAudioSource = GetAvailableAudioSource();
		audioSources[availableAudioSource].clip = clip;
		audioSources[availableAudioSource].volume = soundVolume * vol;
		originalVolumes[availableAudioSource] = vol;
		audioTypes[availableAudioSource] = "sound";
		audioSources[availableAudioSource].pitch = pitch;
		audioSources[availableAudioSource].panStereo = pan * panMax;
		audioSources[availableAudioSource].Play();
		pauseRecord[availableAudioSource] = false;
		return audioSources[availableAudioSource];
	}

	public AudioSource PlayMusic(AudioClip clip, float vol, float pitch, float pan)
	{
		int num = 1;
		audioSources[num].clip = clip;
		audioSources[num].volume = musicVolume * vol;
		originalVolumes[num] = vol;
		audioTypes[num] = "music";
		audioSources[num].pitch = pitch;
		audioSources[num].panStereo = pan * panMax;
		audioSources[num].Play();
		return audioSources[num];
	}

	public AudioSource PlayNarration(AudioClip clip, float vol, float pitch, float pan)
	{
		int num = 0;
		audioSources[num].clip = clip;
		audioSources[num].volume = narrationVolume * vol;
		originalVolumes[num] = vol;
		audioTypes[num] = "narration";
		audioSources[num].pitch = pitch;
		audioSources[num].panStereo = pan * panMax;
		audioSources[num].Play();
		return audioSources[num];
	}

	public AudioSource PlayReplacingSameClip(AudioClip clip, float vol, float pitch, float pan)
	{
		int audioSourceWithSameClip = GetAudioSourceWithSameClip(clip);
		if (audioSourceWithSameClip == -1)
		{
			return PlaySound(clip, vol, pitch, pan);
		}
		if (audioSources[audioSourceWithSameClip].timeSamples < 5000)
		{
			return null;
		}
		audioSources[audioSourceWithSameClip].clip = clip;
		audioSources[audioSourceWithSameClip].volume = soundVolume * vol;
		originalVolumes[audioSourceWithSameClip] = vol;
		audioTypes[audioSourceWithSameClip] = "sound";
		audioSources[audioSourceWithSameClip].pitch = pitch;
		audioSources[audioSourceWithSameClip].panStereo = pan * panMax;
		audioSources[audioSourceWithSameClip].Play();
		return audioSources[audioSourceWithSameClip];
	}

	public void UpdatePan(AudioSource audioSource, float pan)
	{
		audioSource.panStereo = pan * panMax;
	}

	private int GetAvailableAudioSource()
	{
		for (int i = 2; i < audioSources.Length; i++)
		{
			if (!audioSources[i].isPlaying && !pauseRecord[i])
			{
				return i;
			}
		}
		return 2;
	}

	private int GetAudioSourceWithSameClip(AudioClip clip)
	{
		for (int i = 0; i < audioSources.Length; i++)
		{
			if (audioSources[i].isPlaying && audioSources[i].clip == clip)
			{
				return i;
			}
		}
		return -1;
	}

	private void InitializeAudioSources()
	{
		audioSources = GetComponents<AudioSource>();
		for (int i = 0; i < audioSources.Length; i++)
		{
			audioSources[i].Stop();
		}
	}

	private void InitializeOriginalVolumes()
	{
		originalVolumes = new float[audioSources.Length];
		for (int i = 0; i < audioSources.Length; i++)
		{
			originalVolumes[i] = 1f;
		}
	}

	private void InitializeAudioTypes()
	{
		audioTypes = new string[audioSources.Length];
		for (int i = 0; i < audioSources.Length; i++)
		{
			audioTypes[i] = "sound";
		}
	}

	private void InitializeReplaces()
	{
		replaces = new bool[audioSources.Length];
		replacesVolume = new float[audioSources.Length];
		replacesPitch = new float[audioSources.Length];
		replacesPan = new float[audioSources.Length];
		for (int i = 0; i < audioSources.Length; i++)
		{
			replaces[i] = false;
			replacesVolume[i] = 1f;
			replacesPitch[i] = 1f;
			replacesPan[i] = 0f;
		}
	}

	public bool IsMusicPlaying()
	{
		if (audioSources[1].isPlaying)
		{
			return true;
		}
		return false;
	}
}
