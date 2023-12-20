using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitlesController : MonoBehaviour
{
	public static SubtitlesController subtitlesController;

	public Dictionary<string, TextString> textStrings;

	public TextMeshProUGUI subtitles;

	private int lineNumber;

	private string narration = string.Empty;

	private void Start()
	{
		subtitlesController = this;
		ShowOrHideSubtitles();
	}

	public void ShowOrHideSubtitles()
	{
		if (PlayerPrefs.GetInt("subtitles") == 1)
		{
			subtitles.enabled = true;
			if (lineNumber > 0)
			{
				ShowCurrentLine();
			}
		}
		else
		{
			subtitles.enabled = false;
		}
	}

	private void ShowNewLine()
	{
		string text = string.Empty;
		lineNumber++;
		string text2 = lineNumber + string.Empty;
		if (text2.Length < 2)
		{
			text2 = "0" + text2;
		}
		if (GlobalSettings.globalSettings.language == "en")
		{
			text = textStrings["_narration_" + narration + "_" + text2].en;
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			text = textStrings["_narration_" + narration + "_" + text2].es;
		}
		subtitles.SetText(text);
	}

	public void ShowCurrentLine()
	{
		string text = string.Empty;
		string text2 = lineNumber + string.Empty;
		if (text2.Length < 2)
		{
			text2 = "0" + text2;
		}
		if (GlobalSettings.globalSettings.language == "en")
		{
			text = textStrings["_narration_" + narration + "_" + text2].en;
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			text = textStrings["_narration_" + narration + "_" + text2].es;
		}
		subtitles.SetText(text);
	}

	private void EndNarration()
	{
		subtitles.SetText(string.Empty);
		lineNumber = 0;
		narration = string.Empty;
	}

	private void SetNarration01()
	{
		narration = "01";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration02()
	{
		narration = "02";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration03()
	{
		narration = "03";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration04()
	{
		narration = "04";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration05()
	{
		narration = "05";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration06()
	{
		narration = "06";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration07()
	{
		narration = "07";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarration08()
	{
		narration = "08";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarrationGirl()
	{
		narration = "girl";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarrationFall()
	{
		narration = "fall";
		textStrings = TranslationController.translationController.textStrings;
	}

	private void SetNarrationJourney()
	{
		narration = "journey";
		textStrings = TranslationController.translationController.textStrings;
	}
}
