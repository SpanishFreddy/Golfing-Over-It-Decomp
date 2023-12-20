using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSettings : MonoBehaviour
{
	public static GameObject control;

	public static GlobalSettings globalSettings;

	public bool mobile;

	public bool steam;

	public string language;

	public bool subtitles;

	public float cursorSentitivity;

	public float soundsVolume;

	public float linearSoundsVolume;

	public float musicVolume;

	public float linearMusicVolume;

	public float narrationVolume;

	public float linearNarrationVolume;

	private int saveslot;

	public GameObject steamManagerPrefab;

	private void Awake()
	{
		if (control == null)
		{
			control = base.gameObject;
			globalSettings = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (globalSettings != base.gameObject)
		{
			Object.Destroy(base.gameObject);
		}
		GetSavedSettings();
		steam = true;
		if (steam)
		{
			Object.Instantiate(steamManagerPrefab);
		}
	}

	private void Start()
	{
		SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
	}

	public void ChangeSaveslot(int newSaveslot)
	{
		saveslot = newSaveslot;
	}

	public int GetSaveslot()
	{
		return saveslot;
	}

	public void GetSavedSettings()
	{
		if (PlayerPrefs.GetString("language") == string.Empty)
		{
			GetDefaultValues();
			return;
		}
		language = PlayerPrefs.GetString("language");
		if (!mobile)
		{
			ApplySavedResolution();
		}
		if (PlayerPrefs.GetInt("subtitles") == 0)
		{
			subtitles = false;
		}
		else
		{
			subtitles = true;
		}
		cursorSentitivity = (float)PlayerPrefs.GetInt("cursorSensitivity") / 100f;
		linearSoundsVolume = PlayerPrefs.GetInt("soundsVolume");
		soundsVolume = Mathf.Pow(linearSoundsVolume / 100f, 1.66f);
		linearMusicVolume = PlayerPrefs.GetInt("musicVolume");
		musicVolume = Mathf.Pow(linearMusicVolume / 100f, 1.66f);
		linearNarrationVolume = PlayerPrefs.GetInt("narrationVolume");
		narrationVolume = Mathf.Pow(linearNarrationVolume / 100f, 1.66f);
	}

	public void GetDefaultValues()
	{
		if (Application.systemLanguage == SystemLanguage.Spanish)
		{
			PlayerPrefs.SetString("language", "es");
		}
		else if (Application.systemLanguage == SystemLanguage.Catalan)
		{
			PlayerPrefs.SetString("language", "es");
		}
		else if (Application.systemLanguage == SystemLanguage.Basque)
		{
			PlayerPrefs.SetString("language", "es");
		}
		else
		{
			PlayerPrefs.SetString("language", "en");
		}
		language = PlayerPrefs.GetString("language");
		PlayerPrefs.SetInt("resolutionX", Screen.width);
		PlayerPrefs.SetInt("resolutionY", Screen.height);
		PlayerPrefs.SetInt("fullscreen", 1);
		PlayerPrefs.SetInt("subtitles", 1);
		subtitles = true;
		PlayerPrefs.SetInt("cursorSensitivity", 100);
		cursorSentitivity = 100f;
		PlayerPrefs.SetInt("soundsVolume", 100);
		linearSoundsVolume = 100f;
		soundsVolume = Mathf.Pow(linearSoundsVolume / 100f, 1.66f);
		PlayerPrefs.SetInt("musicVolume", 100);
		linearMusicVolume = 100f;
		musicVolume = Mathf.Pow(linearMusicVolume / 100f, 1.66f);
		PlayerPrefs.SetInt("narrationVolume", 100);
		linearNarrationVolume = 100f;
		narrationVolume = Mathf.Pow(linearNarrationVolume / 100f, 1.66f);
	}

	private void ApplySavedResolution()
	{
		Screen.SetResolution(fullscreen: (PlayerPrefs.GetInt("fullscreen") != 0) ? true : false, width: PlayerPrefs.GetInt("resolutionX"), height: PlayerPrefs.GetInt("resolutionY"));
	}
}
