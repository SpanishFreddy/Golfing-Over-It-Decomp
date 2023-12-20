using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingCreditsAnimationController : MonoBehaviour
{
	public Dictionary<string, TextString> textStrings;

	public TextMeshProUGUI creditsText;

	private int lineNumber;

	private void Start()
	{
		textStrings = TranslationController.translationController.textStrings;
	}

	private void ShowNewLine()
	{
		string text = string.Empty;
		lineNumber++;
		string text2 = lineNumber + string.Empty;
		if (GlobalSettings.globalSettings.language == "en")
		{
			text = textStrings["_credits_" + text2].en;
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			text = textStrings["_credits_" + text2].es;
		}
		creditsText.SetText(text);
	}

	private void ShowTime()
	{
		EndingCreditsController.endingCreditsController.ShowTime();
	}

	private void ShowLogo()
	{
		EndingCreditsController.endingCreditsController.ShowLogo();
	}

	private void ShowTimeAndHits()
	{
		EndingCreditsController.endingCreditsController.ShowTimeAndHits();
	}

	private void ShowGirl()
	{
		if (!BallController.ballController.cannon)
		{
			EndingCreditsController.endingCreditsController.ShowGirl();
		}
	}

	private void EndCredits()
	{
		EndingCreditsController.endingCreditsController.EndCredits();
	}
}
