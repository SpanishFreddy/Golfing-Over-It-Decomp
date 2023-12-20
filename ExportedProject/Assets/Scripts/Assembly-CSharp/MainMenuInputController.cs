using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInputController : MonoBehaviour
{
	public static MainMenuInputController mainMenuInputController;

	public Dictionary<string, TextString> textStrings;

	public GameObject cursor;

	public int selection;

	private string currentMenu = "main";

	public GameObject mainMenu;

	public GameObject mobileMainMenu;

	public GameObject saveslotMenu;

	public GameObject deleteMenu;

	public GameObject creditsContainer;

	public TextMeshProUGUI _mm_save;

	public TextMeshProUGUI _mm_save_delete;

	public TextMeshProUGUI _save_left;

	public TextMeshProUGUI _save_right;

	public TextMeshProUGUI _mm_delete_confirmation;

	public AudioClip swing;

	public AudioClip bounce1;

	public AudioClip bounce2;

	public AudioClip bounce3;

	public AudioClip bounce4;

	public AudioClip bounce5;

	public AudioClip invalid;

	public bool killInput = true;

	private bool usingMouse = true;

	private AsyncOperation asyncLoad;

	private int saveslotSelection;

	private int saveArrowSelection;

	public float saveslotTime;

	public bool saveslotFinished;

	private bool loadingPlay;

	private TextMeshProUGUI[] coloredTextQueue = new TextMeshProUGUI[10];

	private Color32 originalColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	private Color32 pressedColor = new Color32(29, 192, 234, byte.MaxValue);

	private Color32 invalidColor = new Color32(246, 60, 60, byte.MaxValue);

	private int deleteLabelClickCount;

	private bool mouseClickedThisFrame;

	private int mouseClickedFramesAgo;

	private bool playSelectionChangeNextFrame;

	private void Start()
	{
		mainMenuInputController = this;
		selection = 0;
		if (GlobalSettings.globalSettings.mobile)
		{
			mainMenu.SetActive(false);
			mobileMainMenu.SetActive(true);
		}
		else
		{
			mainMenu.SetActive(true);
			mobileMainMenu.SetActive(false);
		}
		Invoke("UnkillInput", 0.1f);
		Cursor.visible = true;
	}

	private void Update()
	{
		ManageInput();
		if (playSelectionChangeNextFrame)
		{
			PlaySelectionChange();
		}
		if (textStrings == null && TranslationController.translationController != null)
		{
			textStrings = TranslationController.translationController.textStrings;
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
	}

	private void UnkillInput()
	{
		killInput = false;
	}

	private void ManageInput()
	{
		if (killInput || loadingPlay)
		{
			return;
		}
		if (currentMenu == "main")
		{
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
					selection = 2;
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
					if (GlobalSettings.globalSettings.mobile)
					{
						PlayInvalid();
						return;
					}
					selection = 4;
					ApplySelection();
					PlaySelectionChange();
				}
				else if (selection == 4)
				{
					PlayInvalid();
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
					PlayInvalid();
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
					selection = 3;
					ApplySelection();
					PlaySelectionChange();
				}
			}
			else if (GlobalInput.globalInput.acceptDown || GlobalInput.globalInput.startDown)
			{
				usingMouse = false;
				if (selection != 0)
				{
					AcceptSelection();
					return;
				}
				selection = 1;
				ApplySelection();
				PlaySelectionChange();
			}
			else if (GlobalInput.globalInput.exitDown)
			{
				usingMouse = false;
				selection = 4;
				ApplySelection();
				PlaySelectionChange();
			}
		}
		else if (currentMenu == "saveslot")
		{
			if (GlobalInput.globalInput.down && GlobalInput.globalInput.stepped)
			{
				usingMouse = false;
				if (selection == 0)
				{
					selection = 2;
					ApplySelection();
					PlaySelectionChange();
				}
				else if (selection == 1)
				{
					selection = 2;
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
					PlayInvalid();
				}
			}
			else if (GlobalInput.globalInput.up && GlobalInput.globalInput.stepped)
			{
				usingMouse = false;
				if (selection == 0)
				{
					selection = 2;
					ApplySelection();
					PlaySelectionChange();
				}
				else if (selection == 1)
				{
					PlayInvalid();
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
					selection = 3;
					ApplySelection();
					PlaySelectionChange();
				}
			}
			else if (GlobalInput.globalInput.acceptDown || GlobalInput.globalInput.startDown)
			{
				usingMouse = false;
				if (selection != 0)
				{
					AcceptSelection();
				}
			}
			else if (GlobalInput.globalInput.left && GlobalInput.globalInput.stepped)
			{
				if (selection == 1)
				{
					SaveslotLeft();
				}
			}
			else if (GlobalInput.globalInput.right && GlobalInput.globalInput.stepped)
			{
				if (selection == 1)
				{
					SaveslotRight();
				}
			}
			else if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.cancelDown)
			{
				usingMouse = false;
				SaveslotMenuBack();
			}
		}
		else if (currentMenu == "delete")
		{
			if (GlobalInput.globalInput.down && GlobalInput.globalInput.stepped)
			{
				usingMouse = false;
				if (selection == 0)
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
					PlayInvalid();
				}
			}
			else if (GlobalInput.globalInput.up && GlobalInput.globalInput.stepped)
			{
				usingMouse = false;
				if (selection == 0)
				{
					selection = 3;
					ApplySelection();
					PlaySelectionChange();
				}
				else if (selection == 3)
				{
					PlayInvalid();
				}
				else if (selection == 4)
				{
					selection = 3;
					ApplySelection();
					PlaySelectionChange();
				}
			}
			else if (GlobalInput.globalInput.acceptDown || GlobalInput.globalInput.startDown)
			{
				usingMouse = false;
				if (selection != 0)
				{
					AcceptSelection();
				}
			}
			else if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.cancelDown)
			{
				usingMouse = false;
				DeleteMenuBack();
			}
		}
		else if (currentMenu == "credits")
		{
			if (GlobalInput.globalInput.acceptDown || GlobalInput.globalInput.startDown)
			{
				usingMouse = false;
				AcceptSelection();
			}
			else if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.cancelDown)
			{
				usingMouse = false;
				AcceptSelection();
			}
		}
	}

	public void ApplySelection()
	{
		if (loadingPlay)
		{
			return;
		}
		Vector3 position = new Vector3(-25.5f, 0f, 0f);
		cursor.transform.localScale = new Vector3(1f, 1f, 1f);
		if (GlobalSettings.globalSettings.mobile && currentMenu == "main")
		{
			switch (selection)
			{
			case 0:
				cursor.transform.localScale = new Vector3(0f, 0f, 0f);
				break;
			case 1:
				position.y = 3.5f;
				cursor.transform.position = position;
				break;
			case 2:
				position.y = -2.5f;
				cursor.transform.position = position;
				break;
			case 3:
				position.y = -8.2f;
				cursor.transform.position = position;
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
			position.y = 4f;
			cursor.transform.position = position;
			break;
		case 2:
			position.y = -0.5f;
			cursor.transform.position = position;
			break;
		case 3:
			position.y = -4.75f;
			cursor.transform.position = position;
			break;
		case 4:
			position.y = -9f;
			cursor.transform.position = position;
			break;
		}
	}

	private void AcceptSelection()
	{
		if (currentMenu == "main")
		{
			switch (selection)
			{
			case 1:
				PlayAccept();
				currentMenu = "saveslot";
				if (usingMouse)
				{
					selection = 0;
				}
				else
				{
					selection = 2;
				}
				ApplySelection();
				mainMenu.SetActive(false);
				mobileMainMenu.SetActive(false);
				saveslotMenu.SetActive(true);
				if (PlayerPrefs.GetInt("LastSave") == 0)
				{
					saveslotSelection = 1;
				}
				else
				{
					saveslotSelection = PlayerPrefs.GetInt("LastSave");
				}
				GlobalSettings.globalSettings.ChangeSaveslot(saveslotSelection);
				LoadSaveslotData();
				break;
			case 2:
				killInput = true;
				HideMainMenu();
				if (usingMouse)
				{
					SettingsInputController.settingsInputController.comingFromMainMenu = true;
					SettingsInputController.settingsInputController.ShowSettings();
				}
				else
				{
					SettingsInputController.settingsInputController.comingFromMainMenu = true;
					SettingsInputController.settingsInputController.ShowSettingsUsingKeyboard();
				}
				PlayAccept();
				break;
			case 3:
				currentMenu = "credits";
				PlayAccept();
				creditsContainer.SetActive(true);
				mainMenu.SetActive(false);
				break;
			case 4:
				PlayAccept();
				killInput = true;
				Invoke("Exit", 0.5f);
				break;
			}
		}
		else if (currentMenu == "saveslot")
		{
			switch (selection)
			{
			case 1:
				if (saveArrowSelection == 1)
				{
					SaveslotLeft();
				}
				else if (saveArrowSelection == 2)
				{
					SaveslotRight();
				}
				break;
			case 2:
				loadingPlay = true;
				PlayAccept();
				killInput = true;
				Invoke("Play", 0.5f);
				break;
			case 3:
				PlayAccept();
				currentMenu = "delete";
				if (usingMouse)
				{
					selection = 0;
				}
				else
				{
					selection = 3;
				}
				ApplySelection();
				saveslotMenu.SetActive(false);
				deleteMenu.SetActive(true);
				break;
			case 4:
				SaveslotMenuBack();
				break;
			}
		}
		else if (currentMenu == "delete")
		{
			switch (selection)
			{
			case 3:
				DeleteMenuBack();
				ResetDeleteConfirmation();
				break;
			case 4:
				PlayAccept();
				currentMenu = "saveslot";
				if (deleteLabelClickCount < 9)
				{
					DeleteSave();
				}
				else if (deleteLabelClickCount == 9)
				{
					DeletePlayerPrefs();
				}
				else if (deleteLabelClickCount == 10)
				{
					DeleteAll();
				}
				ResetDeleteConfirmation();
				deleteMenu.SetActive(false);
				saveslotMenu.SetActive(true);
				if (usingMouse)
				{
					selection = 0;
				}
				else
				{
					selection = 4;
				}
				ApplySelection();
				LoadSaveslotData();
				break;
			}
		}
		else if (currentMenu == "credits")
		{
			currentMenu = "main";
			PlayAccept();
			creditsContainer.SetActive(false);
			if (GlobalSettings.globalSettings.mobile)
			{
				mobileMainMenu.SetActive(true);
			}
			else
			{
				mainMenu.SetActive(true);
			}
		}
	}

	private void SaveslotLeft()
	{
		saveslotSelection--;
		if (saveslotSelection < 1)
		{
			saveslotSelection = 9;
		}
		GlobalSettings.globalSettings.ChangeSaveslot(saveslotSelection);
		LoadSaveslotData();
		PlayAccept();
		ChangeToPressedColor(_save_left);
	}

	private void SaveslotRight()
	{
		saveslotSelection++;
		if (saveslotSelection > 9)
		{
			saveslotSelection = 1;
		}
		GlobalSettings.globalSettings.ChangeSaveslot(saveslotSelection);
		LoadSaveslotData();
		PlayAccept();
		ChangeToPressedColor(_save_right);
	}

	private void SaveslotMenuBack()
	{
		PlayAccept();
		if (usingMouse)
		{
			selection = 0;
		}
		else
		{
			selection = 1;
		}
		ApplySelection();
		currentMenu = "main";
		if (GlobalSettings.globalSettings.mobile)
		{
			mobileMainMenu.SetActive(true);
		}
		else
		{
			mainMenu.SetActive(true);
		}
		saveslotMenu.SetActive(false);
	}

	private void DeleteMenuBack()
	{
		PlayAccept();
		deleteMenu.SetActive(false);
		saveslotMenu.SetActive(true);
		if (usingMouse)
		{
			selection = 0;
		}
		else
		{
			selection = 3;
		}
		ApplySelection();
		currentMenu = "saveslot";
	}

	private void LoadSaveslotData()
	{
		SaveLoad.saveLoad.Load();
		string text = "Save";
		if (GlobalSettings.globalSettings.language == "en")
		{
			text = textStrings["_mm_save"].en;
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			text = textStrings["_mm_save"].es;
		}
		float num = saveslotTime;
		float num2 = Mathf.Floor(num / 60f);
		float num3 = 0f;
		while (num2 > 59f)
		{
			num3 += 1f;
			num2 -= 60f;
		}
		string text2 = num2 + string.Empty;
		if (text2.Length == 1)
		{
			text2 = "0" + text2;
		}
		string text3 = num3 + string.Empty;
		if (text3.Length == 1)
		{
			text3 = "0" + text3;
		}
		if (num > 0f && num < 60f)
		{
			text2 = "01";
		}
		text = text + " " + saveslotSelection + " - " + text3 + ":" + text2;
		if (saveslotFinished)
		{
			text = "*" + text + "*";
		}
		_mm_save.SetText(text);
		_mm_save_delete.SetText(text);
	}

	public void MouseClick()
	{
		if (!loadingPlay && !killInput)
		{
			mouseClickedThisFrame = true;
			mouseClickedFramesAgo = 0;
			AcceptSelection();
		}
	}

	public void MouseExitSelection()
	{
		usingMouse = true;
		selection = 0;
		ApplySelection();
	}

	public void MouseSelectPlay()
	{
		if (selection != 1)
		{
			usingMouse = true;
			selection = 1;
			ApplySelection();
		}
	}

	public void MouseSelectSettings()
	{
		if (selection != 2)
		{
			usingMouse = true;
			selection = 2;
			ApplySelection();
		}
	}

	public void MouseSelectCredits()
	{
		if (selection != 3)
		{
			usingMouse = true;
			selection = 3;
			ApplySelection();
		}
	}

	public void MouseSelectExit()
	{
		if (selection != 4)
		{
			usingMouse = true;
			selection = 4;
			ApplySelection();
		}
	}

	public void MouseSelectSavemenuLeft()
	{
		if (selection != 1 || saveArrowSelection != 1)
		{
			usingMouse = true;
			saveArrowSelection = 1;
			selection = 1;
			ApplySelection();
		}
	}

	public void MouseSelectSavemenuRight()
	{
		if (selection != 1 || saveArrowSelection != 2)
		{
			usingMouse = true;
			saveArrowSelection = 2;
			selection = 1;
			ApplySelection();
		}
	}

	public void MouseSelectSavemenuPlay()
	{
		if (selection != 2)
		{
			usingMouse = true;
			selection = 2;
			ApplySelection();
		}
	}

	public void MouseSelectSavemenuDelete()
	{
		if (selection != 3)
		{
			usingMouse = true;
			selection = 3;
			ApplySelection();
		}
	}

	public void MouseSelectSavemenuBack()
	{
		if (selection != 4)
		{
			usingMouse = true;
			selection = 4;
			ApplySelection();
		}
	}

	public void MouseSelectDeletemenuNo()
	{
		if (selection != 3)
		{
			usingMouse = true;
			selection = 3;
			ApplySelection();
		}
	}

	public void MouseSelectDeletemenuYes()
	{
		if (selection != 4)
		{
			usingMouse = true;
			selection = 4;
			ApplySelection();
		}
	}

	private void DeleteSave()
	{
		SaveLoad.saveLoad.ResetSave();
	}

	private void Play()
	{
		PlayerPrefs.SetInt("LastSave", saveslotSelection);
		GlobalAudio.globalAudio.Initialize();
		SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
	}

	private void Exit()
	{
		Application.Quit();
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
		if (!mouseClickedThisFrame && !loadingPlay)
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

	private void PlayAccept()
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
		Invoke("ChangeToOriginalColor", 0.25f);
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
		Invoke("ChangeToOriginalColor", 0.25f);
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

	private void HideMainMenu()
	{
		cursor.transform.localScale = new Vector3(0f, 0f, 0f);
		mainMenu.SetActive(false);
		mobileMainMenu.SetActive(false);
	}

	public void ShowMainMenu()
	{
		if (usingMouse)
		{
			selection = 0;
		}
		ApplySelection();
		cursor.transform.localScale = new Vector3(1f, 1f, 1f);
		MainMenuController.mainMenuController.Translate();
		if (GlobalSettings.globalSettings.mobile)
		{
			mobileMainMenu.SetActive(true);
		}
		else
		{
			mainMenu.SetActive(true);
		}
	}

	public void ClickDeleteLabel()
	{
		deleteLabelClickCount++;
		PlaySelectionChange();
		if (deleteLabelClickCount == 9)
		{
			string language = GlobalSettings.globalSettings.language;
			if (language == "en")
			{
				_mm_delete_confirmation.SetText(textStrings["_mm_delete_settings"].en);
			}
			else if (language == "es")
			{
				_mm_delete_confirmation.SetText(textStrings["_mm_delete_settings"].es);
			}
		}
		else if (deleteLabelClickCount == 10)
		{
			string language2 = GlobalSettings.globalSettings.language;
			if (language2 == "en")
			{
				_mm_delete_confirmation.SetText(textStrings["_mm_delete_all"].en);
			}
			else if (language2 == "es")
			{
				_mm_delete_confirmation.SetText(textStrings["_mm_delete_all"].es);
			}
		}
		else if (deleteLabelClickCount == 11)
		{
			ResetDeleteConfirmation();
		}
	}

	private void ResetDeleteConfirmation()
	{
		deleteLabelClickCount = 0;
		string language = GlobalSettings.globalSettings.language;
		if (language == "en")
		{
			_mm_delete_confirmation.SetText(textStrings["_mm_delete_confirmation"].en);
		}
		else if (language == "es")
		{
			_mm_delete_confirmation.SetText(textStrings["_mm_delete_confirmation"].es);
		}
	}

	private void DeletePlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

	private void DeleteAll()
	{
		DeletePlayerPrefs();
		for (int i = 1; i < 10; i++)
		{
			GlobalSettings.globalSettings.ChangeSaveslot(i);
			SaveLoad.saveLoad.ResetSave();
		}
		killInput = true;
		Object.Destroy(GameObject.Find("GlobalScripter"));
		SceneManager.LoadSceneAsync("LoadingLogo");
	}

	public void MouseClickPolicy()
	{
		Application.OpenURL("https://tapcore.com/en/privacy-policy");
	}
}
