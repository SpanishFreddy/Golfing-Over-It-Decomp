using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	public static MainMenuController mainMenuController;

	public Dictionary<string, TextString> textStrings;

	public TextMeshProUGUI _mm_play;

	public TextMeshProUGUI _mm_settings;

	public TextMeshProUGUI _mm_credits;

	public TextMeshProUGUI _mm_exit;

	public TextMeshProUGUI _mm_play_mobile;

	public TextMeshProUGUI _mm_settings_mobile;

	public TextMeshProUGUI _mm_credits_mobile;

	public TextMeshProUGUI _saveslot_play;

	public TextMeshProUGUI _saveslot_delete;

	public TextMeshProUGUI _saveslot_back;

	public TextMeshProUGUI _delete_confirmation;

	public TextMeshProUGUI _delete_yes;

	public TextMeshProUGUI _delete_no;

	public TextMeshProUGUI _credits;

	public TextMeshProUGUI _policy;

	public GameObject privacyPolicy;

	public AudioClip wind;

	private bool translated;

	private void Start()
	{
		mainMenuController = this;
		if (GlobalAudio.globalAudio != null)
		{
			GlobalAudio.globalAudio.Initialize();
		}
	}

	private void Update()
	{
		if (!translated && TranslationController.translationController != null && TranslationController.translationController.textLoaded)
		{
			Translate();
			GlobalAudio.globalAudio.PlaySound(wind, 1f, 1f, 0f);
		}
	}

	public void Translate()
	{
		translated = true;
		textStrings = TranslationController.translationController.textStrings;
		string text = string.Empty;
		if (GlobalSettings.globalSettings.language == "en")
		{
			_mm_play.SetText(textStrings["_mm_play"].en);
			_mm_settings.SetText(textStrings["_mm_settings"].en);
			_mm_credits.SetText(textStrings["_mm_credits"].en);
			_mm_exit.SetText(textStrings["_mm_exit"].en);
			_mm_play_mobile.SetText(textStrings["_mm_play"].en);
			_mm_settings_mobile.SetText(textStrings["_mm_settings"].en);
			_mm_credits_mobile.SetText(textStrings["_mm_credits"].en);
			_saveslot_play.SetText(textStrings["_mm_play"].en);
			_saveslot_delete.SetText(textStrings["_mm_delete"].en);
			_saveslot_back.SetText(textStrings["_mm_back"].en);
			_delete_confirmation.SetText(textStrings["_mm_delete_confirmation"].en);
			_delete_yes.SetText(textStrings["_mm_delete_yes"].en);
			_delete_no.SetText(textStrings["_mm_delete_no"].en);
			_policy.SetText(textStrings["_mm_privacy_policy"].en);
			for (int i = 1; i < 9; i++)
			{
				text = text + textStrings["_credits_" + i].en + "\n\n";
			}
			_credits.SetText(text);
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			_mm_play.SetText(textStrings["_mm_play"].es);
			_mm_settings.SetText(textStrings["_mm_settings"].es);
			_mm_credits.SetText(textStrings["_mm_credits"].es);
			_mm_exit.SetText(textStrings["_mm_exit"].es);
			_mm_play_mobile.SetText(textStrings["_mm_play"].es);
			_mm_settings_mobile.SetText(textStrings["_mm_settings"].es);
			_mm_credits_mobile.SetText(textStrings["_mm_credits"].es);
			_saveslot_play.SetText(textStrings["_mm_play"].es);
			_saveslot_delete.SetText(textStrings["_mm_delete"].es);
			_saveslot_back.SetText(textStrings["_mm_back"].es);
			_delete_confirmation.SetText(textStrings["_mm_delete_confirmation"].es);
			_delete_yes.SetText(textStrings["_mm_delete_yes"].es);
			_delete_no.SetText(textStrings["_mm_delete_no"].es);
			_policy.SetText(textStrings["_mm_privacy_policy"].es);
			for (int j = 1; j < 9; j++)
			{
				text = text + textStrings["_credits_" + j].es + "\n\n";
			}
			_credits.SetText(text);
		}
	}
}
