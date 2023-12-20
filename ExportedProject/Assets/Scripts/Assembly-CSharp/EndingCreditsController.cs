using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingCreditsController : MonoBehaviour
{
	public static EndingCreditsController endingCreditsController;

	public TextMeshProUGUI credits_text;

	public Dictionary<string, TextString> textStrings;

	public GameObject CreditsContainer;

	public GameObject CreditsCanvas;

	public Camera cam;

	public GameObject majorariattoLogo;

	public GameObject dancingGirlContainer;

	private float timer;

	private float timerStats;

	private bool startCredits;

	private bool cameraPosition;

	private bool stats;

	private Vector3 initialCameraPosition;

	private Vector3 finalCameraPosition;

	public AudioClip music;

	private void Start()
	{
		endingCreditsController = this;
		textStrings = TranslationController.translationController.textStrings;
	}

	private void Update()
	{
		if (startCredits)
		{
			timer += Time.deltaTime;
			timerStats += Time.deltaTime;
		}
		if (timer > 0f && !cameraPosition)
		{
			float t = timer / 4f * (timer / 4f) * (timer / 4f) * (timer / 4f * (6f * (timer / 4f) - 15f) + 10f);
			cam.transform.position = Vector3.Lerp(initialCameraPosition, finalCameraPosition, t);
			if (cam.transform.position == finalCameraPosition)
			{
				cameraPosition = true;
			}
		}
		if (timerStats > 5f && !stats)
		{
			stats = true;
			CreditsContainer.GetComponent<Animator>().SetTrigger("start");
		}
	}

	public void StartCredits()
	{
		NarrationController.narrationController.ClearQueue();
		cam.GetComponent<CameraController>().enabled = false;
		initialCameraPosition = cam.transform.position;
		finalCameraPosition = new Vector3(150f, 768f, cam.transform.position.z);
		startCredits = true;
		GlobalAudio.globalAudio.fadeMusic = false;
		GlobalAudio.globalAudio.PlayMusic(music, 1f, 1f, 0f);
		CanonController.cannonController.HideGirl();
	}

	public void MoveCameraAgain()
	{
		timer = 0f;
		cameraPosition = false;
		cam.GetComponent<CameraController>().enabled = false;
	}

	public void EndCredits()
	{
		cam.GetComponent<CameraController>().enabled = true;
	}

	public void ShowTime()
	{
		credits_text.SetText(GetTimeStat());
		CreditsCanvas.SetActive(true);
	}

	public void ShowLogo()
	{
		credits_text.SetText(string.Empty);
		majorariattoLogo.SetActive(true);
	}

	public void ShowTimeAndHits()
	{
		majorariattoLogo.SetActive(false);
		credits_text.SetText(GetTimeStat());
		CreditsCanvas.SetActive(true);
	}

	public void ShowGirl()
	{
		dancingGirlContainer.SetActive(true);
		dancingGirlContainer.GetComponent<Animator>().SetTrigger("start");
	}

	private string GetTimeStat()
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = string.Empty;
		if (GlobalSettings.globalSettings.language == "en")
		{
			text = textStrings["_time"].en;
			text2 = textStrings["_hits"].en;
			text3 = textStrings["_abbreviation_hours"].en;
			text4 = textStrings["_abbreviation_minutes"].en;
			text5 = textStrings["_abbreviation_seconds"].en;
		}
		else if (GlobalSettings.globalSettings.language == "es")
		{
			text = textStrings["_time"].es;
			text2 = textStrings["_hits"].es;
			text3 = textStrings["_abbreviation_hours"].es;
			text4 = textStrings["_abbreviation_minutes"].es;
			text5 = textStrings["_abbreviation_seconds"].es;
		}
		float globalTimer = BallController.ballController.globalTimer;
		string text6 = globalTimer + string.Empty;
		string[] array = text6.Split("."[0]);
		if (array.Length == 1)
		{
			text6 = "000";
		}
		else
		{
			text6 = array[1];
			if (text6.Length > 3)
			{
				text6 = text6.Substring(0, 3);
			}
			else if (text6.Length == 2)
			{
				text6 += "0";
			}
			else if (text6.Length == 1)
			{
				text6 += "00";
			}
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
		string text7 = num + string.Empty;
		if (text7.Length == 1)
		{
			text7 = "0" + text7;
		}
		string text8 = num2 + string.Empty;
		if (text8.Length == 1)
		{
			text8 = "0" + text8;
		}
		string text9 = num3 + string.Empty;
		if (text9.Length == 1)
		{
			text9 = "0" + text9;
		}
		string text10 = text9 + text3 + ":" + text8 + text4 + ":" + text7 + "." + text6 + text5;
		return text + ":\u00a0" + text10 + "\n" + text2 + ":\u00a0" + BallController.ballController.hits;
	}
}
