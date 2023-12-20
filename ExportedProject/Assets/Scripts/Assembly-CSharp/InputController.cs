using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
	public static InputController inputController;

	public GameObject menu_icon;

	public bool killInput = true;

	private void Start()
	{
		inputController = this;
		if (GlobalSettings.globalSettings.mobile)
		{
			menu_icon.SetActive(true);
		}
	}

	private void Update()
	{
		if (killInput)
		{
			return;
		}
		if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.startDown || GlobalInput.globalInput.rightClickDown)
		{
			if (!SettingsInputController.settingsInputController.settingsCamera.enabled)
			{
				ShowSettings();
				SettingsInputController.settingsInputController.PlayAccept();
			}
			else if (GlobalInput.globalInput.exitDown || GlobalInput.globalInput.rightClickDown)
			{
				HideSettings();
			}
		}
		else if (Input.GetKeyUp("p") && !BallController.ballController.killInput)
		{
			if (Time.timeScale > 0f)
			{
				Time.timeScale = 0f;
				GlobalAudio.globalAudio.PauseAll();
			}
			else
			{
				Time.timeScale = 1f;
				GlobalAudio.globalAudio.UnpauseAll();
			}
		}
	}

	private void ShowSettings()
	{
		killInput = true;
		SaveLoad.saveLoad.Save();
		SettingsInputController.settingsInputController.comingFromMainMenu = false;
		SettingsInputController.settingsInputController.ShowSettingsUsingKeyboard();
		BallController.ballController.killInput = true;
		Time.timeScale = 0f;
	}

	private void HideSettings()
	{
		SettingsInputController.settingsInputController.HideSettings();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void ToggleSettings()
	{
		if (!SettingsInputController.settingsInputController.settingsCamera.enabled)
		{
			killInput = true;
			SaveLoad.saveLoad.Save();
			SettingsInputController.settingsInputController.comingFromMainMenu = false;
			SettingsInputController.settingsInputController.ShowSettings();
			BallController.ballController.killInput = true;
			Time.timeScale = 0f;
		}
		else
		{
			HideSettings();
		}
		menu_icon.GetComponent<Image>().color = new Color32(0, 213, byte.MaxValue, 100);
		StartCoroutine(ReturnIconColor());
	}

	private IEnumerator ReturnIconColor()
	{
		float timeElapsed = 0f;
		while (timeElapsed < 0.1f)
		{
			timeElapsed += Time.unscaledDeltaTime;
			yield return null;
		}
		menu_icon.GetComponent<Image>().color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 100);
	}
}
