using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsInputController : MonoBehaviour
{
	public static SettingsInputController settingsInputController;

	public Dictionary<string, TextString> textStrings;

	public Camera settingsCamera;

	public GameObject cursor;

	private int selection;

	private int arrowSelection;

	public GameObject canvas;

	public GameObject mmPanel;

	public GameObject PCSettings;

	public GameObject mobileSettings;

	public AudioClip swing;

	public AudioClip bounce1;

	public AudioClip bounce2;

	public AudioClip bounce3;

	public AudioClip bounce4;

	public AudioClip bounce5;

	public AudioClip invalid;

	private bool killInput = true;

	private bool usingMouse = true;

	private int saveslotSelection;

	private string[] languages;

	private string[] languageCodes;

	private int languageSelection;

	private Resolution[] resolutions;

	private int resolutionSelection;

	private int fullscreenSelection;

	private int subtitlesSelection;

	private int cursorSensitivity;

	private int soundsVolume;

	private int musicVolume;

	private int narrationVolume;

	public TextMeshProUGUI _time_label;

	public TextMeshProUGUI _time_info;

	public TextMeshProUGUI _hits_label;

	public TextMeshProUGUI _hits_info;

	public TextMeshProUGUI _language_label_pc;

	public TextMeshProUGUI _language_info_pc;

	public TextMeshProUGUI _language_right;

	public TextMeshProUGUI _language_left;

	public TextMeshProUGUI _resolution_label_pc;

	public TextMeshProUGUI _resolution_info_pc;

	public TextMeshProUGUI _resolution_right;

	public TextMeshProUGUI _resolution_left;

	public TextMeshProUGUI _fullscreen_label_pc;

	public TextMeshProUGUI _fullscreen_info_pc;

	public TextMeshProUGUI _fullscreen_right;

	public TextMeshProUGUI _fullscreen_left;

	public TextMeshProUGUI _subtitles_label_pc;

	public TextMeshProUGUI _subtitles_info_pc;

	public TextMeshProUGUI _subtitles_right;

	public TextMeshProUGUI _subtitles_left;

	public TextMeshProUGUI _cursor_sensitivity_label_pc;

	public TextMeshProUGUI _cursor_sensitivity_info_pc;

	public TextMeshProUGUI _cursor_sensitivity_right;

	public TextMeshProUGUI _cursor_sensitivity_left;

	public TextMeshProUGUI _sounds_volume_label_pc;

	public TextMeshProUGUI _sounds_volume_info_pc;

	public TextMeshProUGUI _sounds_volume_right;

	public TextMeshProUGUI _sounds_volume_left;

	public TextMeshProUGUI _music_volume_label_pc;

	public TextMeshProUGUI _music_volume_info_pc;

	public TextMeshProUGUI _music_volume_right;

	public TextMeshProUGUI _music_volume_left;

	public TextMeshProUGUI _narration_volume_label_pc;

	public TextMeshProUGUI _narration_volume_info_pc;

	public TextMeshProUGUI _narration_volume_right;

	public TextMeshProUGUI _narration_volume_left;

	public TextMeshProUGUI _resume_pc;

	public TextMeshProUGUI _apply_pc;

	public TextMeshProUGUI _exit_pc;

	public TextMeshProUGUI _time_label_mobile;

	public TextMeshProUGUI _time_info_mobile;

	public TextMeshProUGUI _hits_label_mobile;

	public TextMeshProUGUI _hits_info_mobile;

	public TextMeshProUGUI _language_label_mobile;

	public TextMeshProUGUI _language_info_mobile;

	public TextMeshProUGUI _language_right_mobile;

	public TextMeshProUGUI _language_left_mobile;

	public TextMeshProUGUI _subtitles_label_mobile;

	public TextMeshProUGUI _subtitles_info_mobile;

	public TextMeshProUGUI _subtitles_right_mobile;

	public TextMeshProUGUI _subtitles_left_mobile;

	public TextMeshProUGUI _cursor_sensitivity_label_mobile;

	public TextMeshProUGUI _cursor_sensitivity_info_mobile;

	public TextMeshProUGUI _cursor_sensitivity_right_mobile;

	public TextMeshProUGUI _cursor_sensitivity_left_mobile;

	public TextMeshProUGUI _sounds_volume_label_mobile;

	public TextMeshProUGUI _sounds_volume_info_mobile;

	public TextMeshProUGUI _sounds_volume_right_mobile;

	public TextMeshProUGUI _sounds_volume_left_mobile;

	public TextMeshProUGUI _music_volume_label_mobile;

	public TextMeshProUGUI _music_volume_info_mobile;

	public TextMeshProUGUI _music_volume_right_mobile;

	public TextMeshProUGUI _music_volume_left_mobile;

	public TextMeshProUGUI _narration_volume_label_mobile;

	public TextMeshProUGUI _narration_volume_info_mobile;

	public TextMeshProUGUI _narration_volume_right_mobile;

	public TextMeshProUGUI _narration_volume_left_mobile;

	public TextMeshProUGUI _resume_mobile;

	public TextMeshProUGUI _apply_mobile;

	public TextMeshProUGUI _exit_mobile;

	public bool comingFromMainMenu = true;

	private TextMeshProUGUI[] coloredTextQueue = new TextMeshProUGUI[10];

	private Color32 originalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private Color32 pressedColor = new Color32(29, 192, 234, byte.MaxValue);

	private Color32 invalidColor = new Color32(246, 60, 60, byte.MaxValue);

	private bool mouseClickedThisFrame;

	private int mouseClickedFramesAgo;

	private bool playSelectionChangeNextFrame;

	private Vector2 currentResolution;

	private void Start()
	{
		HideSettings();
		settingsInputController = this;
		textStrings = TranslationController.translationController.textStrings;
		InitializeVars();
		ApplyChanges();
		GlobalSettings.globalSettings.GetSavedSettings();
		currentResolution = new Vector2(Screen.width, Screen.height);
	}

	private void Update()
	{
		if (canvas.activeSelf)
		{
			ManageInput();
		}
		if (playSelectionChangeNextFrame)
		{
			PlaySelectionChange();
		}
		if (mouseClickedThisFrame && mouseClickedFramesAgo < 5)
		{
			mouseClickedFramesAgo++;
		}
		else if (mouseClickedThisFrame && mouseClickedFramesAgo >= 5)
		{
			mouseClickedThisFrame = false;
			mouseClickedFramesAgo = 0;
		}
		if (!GlobalSettings.globalSettings.mobile)
		{
			ManageWindowResize();
		}
	}

	private void ManageWindowResize()
	{
		if (currentResolution.x == (float)Screen.width && currentResolution.y == (float)Screen.height)
		{
			return;
		}
		currentResolution = new Vector2(Screen.width, Screen.height);
		resolutions = Screen.resolutions;
		List<Resolution> list = new List<Resolution>();
		Resolution item = resolutions[0];
		item.refreshRate = Screen.currentResolution.refreshRate;
		list.Add(item);
		for (int i = 1; i < resolutions.Length; i++)
		{
			if (resolutions[i].width != resolutions[i - 1].width || resolutions[i].height != resolutions[i - 1].height)
			{
				Resolution item2 = resolutions[i];
				item2.refreshRate = Screen.currentResolution.refreshRate;
				list.Add(item2);
			}
		}
		resolutions = new Resolution[list.Count];
		for (int j = 0; j < resolutions.Length; j++)
		{
			resolutions[j] = list[j];
		}
		resolutionSelection = -1;
		for (int k = 0; k < resolutions.Length; k++)
		{
			if (resolutions[k].width == Screen.width && resolutions[k].height == Screen.height)
			{
				resolutionSelection = k;
			}
		}
		if (resolutionSelection == -1)
		{
			int num = 0;
			for (int l = 0; l < resolutions.Length; l++)
			{
				if (resolutions[l].width > Screen.width)
				{
					num = l;
					break;
				}
			}
			Resolution[] array = new Resolution[resolutions.Length + 1];
			for (int m = 0; m < array.Length; m++)
			{
				if (m < num)
				{
					array[m] = resolutions[m];
				}
				else if (m == num)
				{
					Resolution resolution = default(Resolution);
					resolution.width = Screen.width;
					resolution.height = Screen.height;
					resolution.refreshRate = Screen.currentResolution.refreshRate;
					array[m] = resolution;
					resolutionSelection = m;
				}
				else
				{
					array[m] = resolutions[m - 1];
				}
			}
			resolutions = array;
		}
		ChangeResolutionToIndex(resolutionSelection);
		PlayerPrefs.SetInt("resolutionX", resolutions[resolutionSelection].width);
		PlayerPrefs.SetInt("resolutionY", resolutions[resolutionSelection].height);
	}

	private void InitializeVars()
	{
		languages = new string[2] { "English", "Español" };
		languageCodes = new string[2] { "en", "es" };
		string @string = PlayerPrefs.GetString("language");
		if (@string == "en")
		{
			languageSelection = 0;
		}
		else if (@string == "es")
		{
			languageSelection = 1;
		}
		if (!GlobalSettings.globalSettings.mobile)
		{
			resolutions = Screen.resolutions;
			List<Resolution> list = new List<Resolution>();
			Resolution item = resolutions[0];
			item.refreshRate = Screen.currentResolution.refreshRate;
			list.Add(item);
			for (int i = 1; i < resolutions.Length; i++)
			{
				if (resolutions[i].width != resolutions[i - 1].width || resolutions[i].height != resolutions[i - 1].height)
				{
					Resolution item2 = resolutions[i];
					item2.refreshRate = Screen.currentResolution.refreshRate;
					list.Add(item2);
				}
			}
			resolutions = new Resolution[list.Count];
			for (int j = 0; j < resolutions.Length; j++)
			{
				resolutions[j] = list[j];
			}
			resolutionSelection = -1;
			for (int k = 0; k < resolutions.Length; k++)
			{
				if (resolutions[k].width == Screen.width && resolutions[k].height == Screen.height)
				{
					resolutionSelection = k;
				}
			}
			if (resolutionSelection == -1)
			{
				int num = 0;
				for (int l = 0; l < resolutions.Length; l++)
				{
					if (resolutions[l].width > Screen.width)
					{
						num = l;
						break;
					}
				}
				Resolution[] array = new Resolution[resolutions.Length + 1];
				for (int m = 0; m < array.Length; m++)
				{
					if (m < num)
					{
						array[m] = resolutions[m];
					}
					else if (m == num)
					{
						Resolution resolution = default(Resolution);
						resolution.width = Screen.width;
						resolution.height = Screen.height;
						resolution.refreshRate = Screen.currentResolution.refreshRate;
						array[m] = resolution;
						resolutionSelection = m;
					}
					else
					{
						array[m] = resolutions[m - 1];
					}
				}
				resolutions = array;
			}
			if (PlayerPrefs.GetInt("fullscreen") == 0)
			{
				fullscreenSelection = 0;
			}
			else
			{
				fullscreenSelection = 1;
			}
		}
		if (PlayerPrefs.GetInt("subtitles") == 0)
		{
			subtitlesSelection = 0;
		}
		else
		{
			subtitlesSelection = 1;
		}
		cursorSensitivity = PlayerPrefs.GetInt("cursorSensitivity");
		soundsVolume = PlayerPrefs.GetInt("soundsVolume");
		musicVolume = PlayerPrefs.GetInt("musicVolume");
		narrationVolume = PlayerPrefs.GetInt("narrationVolume");
	}

	private void ShowSavedValues()
	{
		if (BallController.ballController != null)
		{
			float globalTimer = BallController.ballController.globalTimer;
			string text = globalTimer + string.Empty;
			string[] array = text.Split("."[0]);
			text = array[1];
			if (text.Length > 3)
			{
				text = text.Substring(0, 3);
			}
			float num = Mathf.Floor(globalTimer);
			float num2 = 0f;
			while (num > 59f)
			{
				num2 += 1f;
				num -= 60f;
			}
			float num3 = 0f;
			while (num2 > 59f)
			{
				num3 += 1f;
				num2 -= 60f;
			}
			string text2 = num + string.Empty;
			if (text2.Length == 1)
			{
				text2 = "0" + text2;
			}
			string text3 = num2 + string.Empty;
			if (text3.Length == 1)
			{
				text3 = "0" + text3;
			}
			string text4 = num3 + string.Empty;
			if (text4.Length == 1)
			{
				text4 = "0" + text4;
			}
			string text5 = text4 + ":" + text3 + ":" + text2;
			_time_info.SetText(text5);
			_hits_info.SetText(BallController.ballController.hits + string.Empty);
			_time_info_mobile.SetText(text5);
			_hits_info_mobile.SetText(BallController.ballController.hits + string.Empty);
		}
		ChangeLanguageToIndex(languageSelection);
		if (!GlobalSettings.globalSettings.mobile)
		{
			ChangeResolutionToIndex(resolutionSelection);
		}
		ChangeFullscreenToIndex(fullscreenSelection);
		ChangeSubtitlesToIndex(subtitlesSelection);
		UpdateCursorSensitivity();
		UpdateSoundsVolume();
		UpdateMusicVolume();
		UpdateNarrationVolume();
	}

	public void ShowSettings()
	{
		selection = 0;
		SharedShowSettings();
	}

	public void ShowSettingsUsingKeyboard()
	{
		selection = 1;
		SharedShowSettings();
	}

	private void SharedShowSettings()
	{
		if (!comingFromMainMenu)
		{
			GlobalAudio.globalAudio.PauseAll();
			mmPanel.SetActive(false);
			if (GlobalSettings.globalSettings.mobile)
			{
				mobileSettings.SetActive(true);
			}
			else
			{
				PCSettings.SetActive(true);
			}
			_time_label.enabled = true;
			_time_info.enabled = true;
			_hits_info.enabled = true;
			_hits_label.enabled = true;
			_exit_pc.enabled = true;
			_exit_mobile.enabled = true;
			SubtitlesController.subtitlesController.subtitles.enabled = false;
			BallController.ballController.DeactivateCursor();
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			mmPanel.SetActive(true);
			if (GlobalSettings.globalSettings.mobile)
			{
				mobileSettings.SetActive(true);
			}
			else
			{
				PCSettings.SetActive(true);
			}
			_time_label.enabled = false;
			_time_info.enabled = false;
			_hits_info.enabled = false;
			_hits_label.enabled = false;
			_exit_pc.enabled = false;
			_exit_mobile.enabled = false;
		}
		InitializeVars();
		ShowSavedValues();
		ChangeLanguageToIndex(languageSelection);
		ApplySelection();
		canvas.SetActive(true);
		cursor.SetActive(true);
		settingsCamera.enabled = true;
		if (CameraController.cameraController != null)
		{
			settingsCamera.transform.position = CameraController.cameraController.gameObject.transform.position;
			settingsCamera.fieldOfView = CameraController.cameraController.gameObject.GetComponent<Camera>().fieldOfView;
			canvas.transform.position = new Vector3(settingsCamera.transform.position.x, settingsCamera.transform.position.y, canvas.transform.position.z);
		}
		else if (MainMenuCameraController.mainMenuCameraController != null)
		{
			settingsCamera.transform.position = MainMenuCameraController.mainMenuCameraController.cam.transform.position;
			settingsCamera.fieldOfView = MainMenuCameraController.mainMenuCameraController.cam.GetComponent<Camera>().fieldOfView;
			canvas.transform.position = new Vector3(settingsCamera.transform.position.x, settingsCamera.transform.position.y, canvas.transform.position.z);
		}
		InvokeRealTime("EnableInput", 0.25f);
	}

	public void InvokeRealTime(string functionName, float delay)
	{
		switch (functionName)
		{
		case "EnableInput":
			StartCoroutine(EnableInputInvoke(delay));
			break;
		case "ExitToMainMenu2":
			StartCoroutine(ExitToMainMenu2Invoke(delay));
			break;
		case "ChangeToOriginalColor":
			StartCoroutine(ChangeToOriginalColorCoroutine(delay));
			break;
		case "UnkillMainMenuInput":
			StartCoroutine(UnkillMainMenuInputCoroutine(delay));
			break;
		case "UnkillBallInput":
			StartCoroutine(UnkillBallInputCoroutine(delay));
			break;
		case "UnkillInputControllerInput":
			StartCoroutine(UnkillInputControllerInputCoroutine(delay));
			break;
		}
	}

	private IEnumerator EnableInputInvoke(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		EnableInput();
	}

	private IEnumerator ExitToMainMenu2Invoke(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		ExitToMainMenu2();
	}

	private void EnableInput()
	{
		killInput = false;
	}

	public void HideSettings()
	{
		PCSettings.SetActive(false);
		mobileSettings.SetActive(false);
		GlobalAudio.globalAudio.UnpauseAll();
		killInput = true;
		canvas.SetActive(false);
		settingsCamera.enabled = false;
		Time.timeScale = 1f;
		if (BallController.ballController != null)
		{
			SubtitlesController.subtitlesController.ShowOrHideSubtitles();
			InvokeRealTime("UnkillBallInput", 0.1f);
			InvokeRealTime("UnkillInputControllerInput", 0.1f);
		}
		else
		{
			MainMenuInputController.mainMenuInputController.ShowMainMenu();
			InvokeRealTime("UnkillMainMenuInput", 0.1f);
		}
	}

	private IEnumerator UnkillMainMenuInputCoroutine(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		UnkillMainMenuInput();
	}

	private IEnumerator UnkillBallInputCoroutine(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		BallController.ballController.killInput = false;
	}

	private IEnumerator UnkillInputControllerInputCoroutine(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		InputController.inputController.killInput = false;
	}

	private void UnkillMainMenuInput()
	{
		MainMenuInputController.mainMenuInputController.killInput = false;
	}

	private void ManageInput()
	{
		if (killInput)
		{
			return;
		}
		if (GlobalInput.globalInput.down && GlobalInput.globalInput.stepped)
		{
			usingMouse = false;
			if (selection == 0)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 1)
			{
				if (GlobalSettings.globalSettings.mobile)
				{
					selection = 4;
				}
				else
				{
					selection = 2;
				}
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 2)
			{
				selection = 3;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 3)
			{
				selection = 4;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 4)
			{
				selection = 5;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 5)
			{
				selection = 6;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 6)
			{
				selection = 7;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 7)
			{
				selection = 8;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 8)
			{
				selection = 9;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 9)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 10)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 11)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
		}
		else if (GlobalInput.globalInput.up && GlobalInput.globalInput.stepped)
		{
			usingMouse = false;
			if (selection == 0)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 1)
			{
				selection = 9;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 2)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 3)
			{
				selection = 2;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 4)
			{
				if (GlobalSettings.globalSettings.mobile)
				{
					selection = 1;
				}
				else
				{
					selection = 3;
				}
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 5)
			{
				selection = 4;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 6)
			{
				selection = 5;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 7)
			{
				selection = 6;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 8)
			{
				selection = 7;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 9)
			{
				selection = 8;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 10)
			{
				selection = 8;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 11)
			{
				selection = 8;
				ApplySelection();
				PlaySelectionChange();
			}
		}
		else if (GlobalInput.globalInput.left && GlobalInput.globalInput.stepped)
		{
			usingMouse = false;
			if (selection == 0)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 1)
			{
				LanguageLeft();
			}
			else if (selection == 2)
			{
				ResolutionLeft();
			}
			else if (selection == 3)
			{
				FullscreenLeft();
			}
			else if (selection == 4)
			{
				SubtitlesLeft();
			}
			else if (selection == 5)
			{
				SensitivityLeft();
			}
			else if (selection == 6)
			{
				SoundsLeft();
			}
			else if (selection == 7)
			{
				MusicLeft();
			}
			else if (selection == 8)
			{
				NarrationLeft();
			}
			else if (selection == 9)
			{
				PlayInvalid();
			}
			else if (selection == 10)
			{
				selection = 9;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 11)
			{
				selection = 10;
				ApplySelection();
				PlaySelectionChange();
			}
		}
		else if (GlobalInput.globalInput.right && GlobalInput.globalInput.stepped)
		{
			usingMouse = false;
			if (selection == 0)
			{
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 1)
			{
				LanguageRight();
			}
			else if (selection == 2)
			{
				ResolutionRight();
			}
			else if (selection == 3)
			{
				FullscreenRight();
			}
			else if (selection == 4)
			{
				SubtitlesRight();
			}
			else if (selection == 5)
			{
				SensitivityRight();
			}
			else if (selection == 6)
			{
				SoundsRight();
			}
			else if (selection == 7)
			{
				MusicRight();
			}
			else if (selection == 8)
			{
				NarrationRight();
			}
			else if (selection == 9)
			{
				selection = 10;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 10)
			{
				if (comingFromMainMenu)
				{
					PlayInvalid();
					return;
				}
				selection = 11;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 11)
			{
				PlayInvalid();
			}
		}
		else if (GlobalInput.globalInput.acceptDown || GlobalInput.globalInput.startDown)
		{
			if (selection != 9 && selection != 10 && selection != 11)
			{
				selection = 10;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (selection == 9)
			{
				AcceptSelection();
			}
			else if (selection == 10)
			{
				selection = 9;
				ApplySelection();
				ApplyChanges();
				PlayAccept();
			}
			else if (selection == 11)
			{
				AcceptSelection();
			}
		}
		else if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.cancelDown || GlobalInput.globalInput.rightClickDown)
		{
			PlayAccept();
			if (comingFromMainMenu)
			{
				HideSettings();
			}
			else
			{
				ExitToGame();
			}
			GlobalSettings.globalSettings.GetSavedSettings();
		}
	}

	private void ApplySelection()
	{
		Vector3 localPosition = new Vector3(-433f, 0f, 0f);
		cursor.transform.localScale = new Vector3(33.33334f, 33.33334f, 1f);
		if (GlobalSettings.globalSettings.mobile)
		{
			switch (selection)
			{
			case 0:
				cursor.transform.localScale = new Vector3(0f, 0f, 0f);
				break;
			case 1:
				localPosition.y = 330f;
				cursor.transform.localPosition = localPosition;
				break;
			case 4:
				localPosition.y = 205f;
				cursor.transform.localPosition = localPosition;
				break;
			case 5:
				localPosition.y = 80f;
				cursor.transform.localPosition = localPosition;
				break;
			case 6:
				localPosition.y = -45f;
				cursor.transform.localPosition = localPosition;
				break;
			case 7:
				localPosition.y = -170f;
				cursor.transform.localPosition = localPosition;
				break;
			case 8:
				localPosition.y = -295f;
				cursor.transform.localPosition = localPosition;
				break;
			case 9:
				localPosition.y = -430f;
				cursor.transform.localPosition = localPosition;
				break;
			case 10:
				localPosition.y = -430f;
				localPosition.x = -50f;
				cursor.transform.localPosition = localPosition;
				break;
			case 11:
				localPosition.y = -430f;
				localPosition.x = 266.6f;
				cursor.transform.localPosition = localPosition;
				break;
			case 2:
			case 3:
				break;
			}
			return;
		}
		switch (selection)
		{
		case 0:
			cursor.transform.localScale = new Vector3(0f, 0f, 0f);
			break;
		case 1:
			localPosition.y = 330f;
			cursor.transform.localPosition = localPosition;
			break;
		case 2:
			localPosition.y = 234f;
			cursor.transform.localPosition = localPosition;
			break;
		case 3:
			localPosition.y = 149f;
			cursor.transform.localPosition = localPosition;
			break;
		case 4:
			localPosition.y = 57f;
			cursor.transform.localPosition = localPosition;
			break;
		case 5:
			localPosition.y = -35f;
			cursor.transform.localPosition = localPosition;
			break;
		case 6:
			localPosition.y = -122f;
			cursor.transform.localPosition = localPosition;
			break;
		case 7:
			localPosition.y = -212f;
			cursor.transform.localPosition = localPosition;
			break;
		case 8:
			localPosition.y = -304f;
			cursor.transform.localPosition = localPosition;
			break;
		case 9:
			localPosition.y = -430f;
			cursor.transform.localPosition = localPosition;
			break;
		case 10:
			localPosition.y = -430f;
			localPosition.x = -50f;
			cursor.transform.localPosition = localPosition;
			break;
		case 11:
			localPosition.y = -430f;
			localPosition.x = 266.6f;
			cursor.transform.localPosition = localPosition;
			break;
		}
	}

	private void AcceptSelection()
	{
		switch (selection)
		{
		case 1:
			if (arrowSelection == 1)
			{
				LanguageLeft();
			}
			else if (arrowSelection == 2)
			{
				LanguageRight();
			}
			break;
		case 2:
			if (arrowSelection == 1)
			{
				ResolutionLeft();
			}
			else if (arrowSelection == 2)
			{
				ResolutionRight();
			}
			break;
		case 3:
			if (arrowSelection == 1)
			{
				FullscreenLeft();
			}
			else if (arrowSelection == 2)
			{
				FullscreenRight();
			}
			break;
		case 4:
			if (arrowSelection == 1)
			{
				SubtitlesLeft();
			}
			else if (arrowSelection == 2)
			{
				SubtitlesRight();
			}
			break;
		case 5:
			if (arrowSelection == 1)
			{
				SensitivityLeft();
			}
			else if (arrowSelection == 2)
			{
				SensitivityRight();
			}
			break;
		case 6:
			if (arrowSelection == 1)
			{
				SoundsLeft();
			}
			else if (arrowSelection == 2)
			{
				SoundsRight();
			}
			break;
		case 7:
			if (arrowSelection == 1)
			{
				MusicLeft();
			}
			else if (arrowSelection == 2)
			{
				MusicRight();
			}
			break;
		case 8:
			if (arrowSelection == 1)
			{
				NarrationLeft();
			}
			else if (arrowSelection == 2)
			{
				NarrationRight();
			}
			break;
		case 9:
			ChangeToPressedColor(_resume_pc);
			ChangeToPressedColor(_resume_mobile);
			if (comingFromMainMenu)
			{
				HideSettings();
			}
			else
			{
				ExitToGame();
			}
			GlobalSettings.globalSettings.GetSavedSettings();
			PlayAccept();
			break;
		case 10:
			ChangeToPressedColor(_apply_pc);
			ChangeToPressedColor(_apply_mobile);
			ApplyChanges();
			PlayAccept();
			break;
		case 11:
			ChangeToPressedColor(_exit_pc);
			ChangeToPressedColor(_exit_mobile);
			ExitToMainMenu();
			PlayAccept();
			break;
		}
	}

	private void LanguageLeft()
	{
		languageSelection--;
		if (languageSelection < 0)
		{
			languageSelection = languages.Length - 1;
		}
		ChangeLanguageToIndex(languageSelection);
		PlayAccept();
		ChangeToPressedColor(_language_left);
		ChangeToPressedColor(_language_left_mobile);
	}

	private void LanguageRight()
	{
		languageSelection++;
		if (languageSelection > languages.Length - 1)
		{
			languageSelection = 0;
		}
		ChangeLanguageToIndex(languageSelection);
		PlayAccept();
		ChangeToPressedColor(_language_right);
		ChangeToPressedColor(_language_right_mobile);
	}

	private void ResolutionLeft()
	{
		if (resolutionSelection > 0)
		{
			resolutionSelection--;
			ChangeResolutionToIndex(resolutionSelection);
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_resolution_left);
	}

	private void ResolutionRight()
	{
		if (resolutionSelection < resolutions.Length - 1)
		{
			resolutionSelection++;
			ChangeResolutionToIndex(resolutionSelection);
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_resolution_right);
	}

	private void FullscreenLeft()
	{
		fullscreenSelection--;
		if (fullscreenSelection < 0)
		{
			fullscreenSelection = 1;
		}
		ChangeFullscreenToIndex(fullscreenSelection);
		PlayAccept();
		ChangeToPressedColor(_fullscreen_left);
	}

	private void FullscreenRight()
	{
		fullscreenSelection++;
		if (fullscreenSelection > 1)
		{
			fullscreenSelection = 0;
		}
		ChangeFullscreenToIndex(fullscreenSelection);
		PlayAccept();
		ChangeToPressedColor(_fullscreen_right);
	}

	private void SubtitlesLeft()
	{
		subtitlesSelection--;
		if (subtitlesSelection < 0)
		{
			subtitlesSelection = 1;
		}
		ChangeSubtitlesToIndex(subtitlesSelection);
		PlayAccept();
		ChangeToPressedColor(_subtitles_left);
		ChangeToPressedColor(_subtitles_left_mobile);
	}

	private void SubtitlesRight()
	{
		subtitlesSelection++;
		if (subtitlesSelection > 1)
		{
			subtitlesSelection = 0;
		}
		ChangeSubtitlesToIndex(subtitlesSelection);
		PlayAccept();
		ChangeToPressedColor(_subtitles_right);
		ChangeToPressedColor(_subtitles_right_mobile);
	}

	private void SensitivityLeft()
	{
		if (cursorSensitivity > 10)
		{
			cursorSensitivity -= 10;
			UpdateCursorSensitivity();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_cursor_sensitivity_left);
		ChangeToPressedColor(_cursor_sensitivity_left_mobile);
	}

	private void SensitivityRight()
	{
		if (cursorSensitivity < 200)
		{
			cursorSensitivity += 10;
			UpdateCursorSensitivity();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_cursor_sensitivity_right);
		ChangeToPressedColor(_cursor_sensitivity_right_mobile);
	}

	private void SoundsLeft()
	{
		if (soundsVolume > 0)
		{
			soundsVolume -= 10;
			UpdateSoundsVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_sounds_volume_left);
		ChangeToPressedColor(_sounds_volume_left_mobile);
	}

	private void SoundsRight()
	{
		if (soundsVolume < 100)
		{
			soundsVolume += 10;
			UpdateSoundsVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_sounds_volume_right);
		ChangeToPressedColor(_sounds_volume_right_mobile);
	}

	private void MusicLeft()
	{
		if (musicVolume > 0)
		{
			musicVolume -= 10;
			UpdateMusicVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_music_volume_left);
		ChangeToPressedColor(_music_volume_left_mobile);
	}

	private void MusicRight()
	{
		if (musicVolume < 100)
		{
			musicVolume += 10;
			UpdateMusicVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_music_volume_right);
		ChangeToPressedColor(_music_volume_right_mobile);
	}

	private void NarrationLeft()
	{
		if (narrationVolume > 0)
		{
			narrationVolume -= 10;
			UpdateNarrationVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_narration_volume_left);
		ChangeToPressedColor(_narration_volume_left_mobile);
	}

	private void NarrationRight()
	{
		if (narrationVolume < 100)
		{
			narrationVolume += 10;
			UpdateNarrationVolume();
			PlayAccept();
		}
		else
		{
			PlayInvalid();
		}
		ChangeToPressedColor(_narration_volume_right_mobile);
	}

	private void ChangeLanguageToIndex(int index)
	{
		if (GlobalSettings.globalSettings.mobile)
		{
			switch (index)
			{
			case 0:
				_time_label_mobile.SetText(textStrings["_time"].en);
				_hits_label_mobile.SetText(textStrings["_hits"].en);
				_language_label_mobile.SetText(textStrings["_settings_language"].en);
				_language_info_mobile.SetText(textStrings["_settings_language_name"].en);
				_subtitles_label_mobile.SetText(textStrings["_settings_subtitles"].en);
				ChangeSubtitlesToIndex(subtitlesSelection);
				_cursor_sensitivity_label_mobile.SetText(textStrings["_settings_cursor_sensitivity"].en);
				_sounds_volume_label_mobile.SetText(textStrings["_settings_sounds_volume"].en);
				_music_volume_label_mobile.SetText(textStrings["_settings_music_volume"].en);
				_narration_volume_label_mobile.SetText(textStrings["_settings_narration_volume"].en);
				if (comingFromMainMenu)
				{
					_resume_mobile.SetText(textStrings["_settings_back"].en);
				}
				else
				{
					_resume_mobile.SetText(textStrings["_settings_resume"].en);
				}
				_apply_mobile.SetText(textStrings["_settings_apply"].en);
				_exit_mobile.SetText(textStrings["_settings_exit"].en);
				break;
			case 1:
				_time_label_mobile.SetText(textStrings["_time"].es);
				_hits_label_mobile.SetText(textStrings["_hits"].es);
				_language_label_mobile.SetText(textStrings["_settings_language"].es);
				_language_info_mobile.SetText(textStrings["_settings_language_name"].es);
				_subtitles_label_mobile.SetText(textStrings["_settings_subtitles"].es);
				ChangeSubtitlesToIndex(subtitlesSelection);
				_cursor_sensitivity_label_mobile.SetText(textStrings["_settings_cursor_sensitivity"].es);
				_sounds_volume_label_mobile.SetText(textStrings["_settings_sounds_volume"].es);
				_music_volume_label_mobile.SetText(textStrings["_settings_music_volume"].es);
				_narration_volume_label_mobile.SetText(textStrings["_settings_narration_volume"].es);
				if (comingFromMainMenu)
				{
					_resume_mobile.SetText(textStrings["_settings_back"].es);
				}
				else
				{
					_resume_mobile.SetText(textStrings["_settings_resume"].es);
				}
				_apply_mobile.SetText(textStrings["_settings_apply"].es);
				_exit_mobile.SetText(textStrings["_settings_exit"].es);
				break;
			}
			return;
		}
		switch (index)
		{
		case 0:
			_time_label.SetText(textStrings["_time"].en);
			_hits_label.SetText(textStrings["_hits"].en);
			_language_label_pc.SetText(textStrings["_settings_language"].en);
			_language_info_pc.SetText(textStrings["_settings_language_name"].en);
			_resolution_label_pc.SetText(textStrings["_settings_resolution"].en);
			_fullscreen_label_pc.SetText(textStrings["_settings_fullscreen"].en);
			ChangeFullscreenToIndex(fullscreenSelection);
			_subtitles_label_pc.SetText(textStrings["_settings_subtitles"].en);
			ChangeSubtitlesToIndex(subtitlesSelection);
			_cursor_sensitivity_label_pc.SetText(textStrings["_settings_cursor_sensitivity"].en);
			_sounds_volume_label_pc.SetText(textStrings["_settings_sounds_volume"].en);
			_music_volume_label_pc.SetText(textStrings["_settings_music_volume"].en);
			_narration_volume_label_pc.SetText(textStrings["_settings_narration_volume"].en);
			if (comingFromMainMenu)
			{
				_resume_pc.SetText(textStrings["_settings_back"].en);
			}
			else
			{
				_resume_pc.SetText(textStrings["_settings_resume"].en);
			}
			_apply_pc.SetText(textStrings["_settings_apply"].en);
			_exit_pc.SetText(textStrings["_settings_exit"].en);
			break;
		case 1:
			_time_label.SetText(textStrings["_time"].es);
			_hits_label.SetText(textStrings["_hits"].es);
			_language_label_pc.SetText(textStrings["_settings_language"].es);
			_language_info_pc.SetText(textStrings["_settings_language_name"].es);
			_resolution_label_pc.SetText(textStrings["_settings_resolution"].es);
			_fullscreen_label_pc.SetText(textStrings["_settings_fullscreen"].es);
			ChangeFullscreenToIndex(fullscreenSelection);
			_subtitles_label_pc.SetText(textStrings["_settings_subtitles"].es);
			ChangeSubtitlesToIndex(subtitlesSelection);
			_cursor_sensitivity_label_pc.SetText(textStrings["_settings_cursor_sensitivity"].es);
			_sounds_volume_label_pc.SetText(textStrings["_settings_sounds_volume"].es);
			_music_volume_label_pc.SetText(textStrings["_settings_music_volume"].es);
			_narration_volume_label_pc.SetText(textStrings["_settings_narration_volume"].es);
			if (comingFromMainMenu)
			{
				_resume_pc.SetText(textStrings["_settings_back"].es);
			}
			else
			{
				_resume_pc.SetText(textStrings["_settings_resume"].es);
			}
			_apply_pc.SetText(textStrings["_settings_apply"].es);
			_exit_pc.SetText(textStrings["_settings_exit"].es);
			break;
		}
	}

	private void ChangeResolutionToIndex(int index)
	{
		_resolution_info_pc.SetText(resolutions[index].width + "x" + resolutions[index].height);
	}

	private void ChangeFullscreenToIndex(int index)
	{
		if (languageSelection == 0)
		{
			if (index == 0)
			{
				_fullscreen_info_pc.SetText(textStrings["_settings_no"].en);
			}
			else
			{
				_fullscreen_info_pc.SetText(textStrings["_settings_yes"].en);
			}
		}
		else if (languageSelection == 1)
		{
			if (index == 0)
			{
				_fullscreen_info_pc.SetText(textStrings["_settings_no"].es);
			}
			else
			{
				_fullscreen_info_pc.SetText(textStrings["_settings_yes"].es);
			}
		}
	}

	private void ChangeSubtitlesToIndex(int index)
	{
		if (languageSelection == 0)
		{
			if (index == 0)
			{
				_subtitles_info_pc.SetText(textStrings["_settings_no"].en);
				_subtitles_info_mobile.SetText(textStrings["_settings_no"].en);
			}
			else
			{
				_subtitles_info_pc.SetText(textStrings["_settings_yes"].en);
				_subtitles_info_mobile.SetText(textStrings["_settings_yes"].en);
			}
		}
		else if (languageSelection == 1)
		{
			if (index == 0)
			{
				_subtitles_info_pc.SetText(textStrings["_settings_no"].es);
				_subtitles_info_mobile.SetText(textStrings["_settings_no"].es);
			}
			else
			{
				_subtitles_info_pc.SetText(textStrings["_settings_yes"].es);
				_subtitles_info_mobile.SetText(textStrings["_settings_yes"].es);
			}
		}
	}

	private void UpdateCursorSensitivity()
	{
		_cursor_sensitivity_info_pc.SetText(cursorSensitivity + "%");
		_cursor_sensitivity_info_mobile.SetText(cursorSensitivity + "%");
	}

	private void UpdateSoundsVolume()
	{
		_sounds_volume_info_pc.SetText(soundsVolume + "%");
		_sounds_volume_info_mobile.SetText(soundsVolume + "%");
		GlobalSettings.globalSettings.soundsVolume = Mathf.Pow((float)soundsVolume / 100f, 1.66f);
	}

	private void UpdateMusicVolume()
	{
		_music_volume_info_pc.SetText(musicVolume + "%");
		_music_volume_info_mobile.SetText(musicVolume + "%");
	}

	private void UpdateNarrationVolume()
	{
		_narration_volume_info_pc.SetText(narrationVolume + "%");
		_narration_volume_info_mobile.SetText(narrationVolume + "%");
	}

	private void ApplyChanges()
	{
		ChangeToPressedColor(_apply_pc);
		PlayerPrefs.SetString("language", languageCodes[languageSelection]);
		if (!GlobalSettings.globalSettings.mobile)
		{
			PlayerPrefs.SetInt("resolutionX", resolutions[resolutionSelection].width);
			PlayerPrefs.SetInt("resolutionY", resolutions[resolutionSelection].height);
			PlayerPrefs.SetInt("fullscreen", fullscreenSelection);
		}
		PlayerPrefs.SetInt("subtitles", subtitlesSelection);
		PlayerPrefs.SetInt("cursorSensitivity", cursorSensitivity);
		PlayerPrefs.SetInt("soundsVolume", soundsVolume);
		PlayerPrefs.SetInt("musicVolume", musicVolume);
		PlayerPrefs.SetInt("narrationVolume", narrationVolume);
		GlobalSettings.globalSettings.GetSavedSettings();
	}

	private void ExitToMainMenu()
	{
		ChangeToPressedColor(_exit_pc);
		killInput = true;
		HideSettings();
		InvokeRealTime("ExitToMainMenu2", 0.1f);
	}

	private void ExitToMainMenu2()
	{
		SceneManager.UnloadSceneAsync("Scene");
		SceneManager.LoadSceneAsync("Title", LoadSceneMode.Additive);
	}

	private void ExitToGame()
	{
		ChangeToPressedColor(_resume_pc);
		HideSettings();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void PlayInvalid()
	{
		GlobalAudio.globalAudio.PlaySound(invalid, 1f, 1f, 0f);
	}

	public void PlaySelectionChange()
	{
		if (!playSelectionChangeNextFrame)
		{
			playSelectionChangeNextFrame = true;
			return;
		}
		playSelectionChangeNextFrame = false;
		if (!mouseClickedThisFrame)
		{
			AudioClip clip = bounce1;
			switch (Random.Range(1, 6))
			{
			case 1:
				clip = bounce1;
				break;
			case 2:
				clip = bounce2;
				break;
			case 3:
				clip = bounce3;
				break;
			case 4:
				clip = bounce4;
				break;
			case 5:
				clip = bounce5;
				break;
			}
			GlobalAudio.globalAudio.PlaySound(clip, 1f, Random.Range(0.75f, 1.25f), 0f);
		}
	}

	public void PlayAccept()
	{
		GlobalAudio.globalAudio.PlaySound(swing, 1f, Random.Range(0.75f, 1.25f), 0f);
	}

	private void ChangeToPressedColor(TextMeshProUGUI txt)
	{
		txt.color = pressedColor;
		for (int i = 0; i < coloredTextQueue.Length; i++)
		{
			if (coloredTextQueue[i] == null)
			{
				coloredTextQueue[i] = txt;
				break;
			}
		}
		InvokeRealTime("ChangeToOriginalColor", 0.25f);
	}

	private void ChangeToInvalidColor(TextMeshProUGUI txt)
	{
		txt.color = invalidColor;
		for (int i = 0; i < coloredTextQueue.Length; i++)
		{
			if (coloredTextQueue[i] == null)
			{
				coloredTextQueue[i] = txt;
				break;
			}
		}
		InvokeRealTime("ChangeToOriginalColor", 0.25f);
	}

	private IEnumerator ChangeToOriginalColorCoroutine(float delay)
	{
		float timeElapsed = 0f;
		while (timeElapsed < delay)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		ChangeToOriginalColor();
	}

	private void ChangeToOriginalColor()
	{
		for (int i = 1; i < coloredTextQueue.Length; i++)
		{
			if (coloredTextQueue[0] == coloredTextQueue[i])
			{
				for (int j = 1; j < coloredTextQueue.Length; j++)
				{
					coloredTextQueue[j - 1] = coloredTextQueue[j];
				}
				return;
			}
		}
		coloredTextQueue[0].color = originalColor;
		for (int k = 1; k < coloredTextQueue.Length; k++)
		{
			coloredTextQueue[k - 1] = coloredTextQueue[k];
		}
		coloredTextQueue[coloredTextQueue.Length - 1] = null;
	}

	public void MouseClick()
	{
		AcceptSelection();
		mouseClickedThisFrame = true;
		mouseClickedFramesAgo = 0;
	}

	public void MouseLanguageLeft()
	{
		usingMouse = true;
		selection = 1;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseLanguageRight()
	{
		usingMouse = true;
		selection = 1;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseResolutionLeft()
	{
		usingMouse = true;
		selection = 2;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseResolutionRight()
	{
		usingMouse = true;
		selection = 2;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseFullscreenLeft()
	{
		usingMouse = true;
		selection = 3;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseFullscreenRight()
	{
		usingMouse = true;
		selection = 3;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseSubtitlesLeft()
	{
		usingMouse = true;
		selection = 4;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseSubtitlesRight()
	{
		usingMouse = true;
		selection = 4;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseSensitivityLeft()
	{
		usingMouse = true;
		selection = 5;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseSensitivityRight()
	{
		usingMouse = true;
		selection = 5;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseSoundsLeft()
	{
		usingMouse = true;
		selection = 6;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseSoundsRight()
	{
		usingMouse = true;
		selection = 6;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseMusicLeft()
	{
		usingMouse = true;
		selection = 7;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseMusicRight()
	{
		usingMouse = true;
		selection = 7;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseNarrationLeft()
	{
		usingMouse = true;
		selection = 8;
		arrowSelection = 1;
		ApplySelection();
	}

	public void MouseNarrationRight()
	{
		usingMouse = true;
		selection = 8;
		arrowSelection = 2;
		ApplySelection();
	}

	public void MouseResume()
	{
		usingMouse = true;
		selection = 9;
		ApplySelection();
	}

	public void MouseApply()
	{
		usingMouse = true;
		selection = 10;
		ApplySelection();
	}

	public void MouseExit()
	{
		if (!comingFromMainMenu)
		{
			usingMouse = true;
			selection = 11;
			ApplySelection();
		}
	}

	public void MouseExitSelection()
	{
		usingMouse = true;
		selection = 0;
		ApplySelection();
	}
}
