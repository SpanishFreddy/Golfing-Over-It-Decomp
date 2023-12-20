using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
	public static SaveLoad saveLoad;

	private int saveFile;

	private string savesPath;

	private void Start()
	{
		saveLoad = this;
		savesPath = Application.persistentDataPath;
	}

	public void Save()
	{
		if (BallController.ballController != null && BallController.ballController.stopSaving)
		{
			return;
		}
		SaveData saveData = new SaveData();
		BallController ballController = BallController.ballController;
		NarrationController narrationController = NarrationController.narrationController;
		saveFile = GlobalSettings.globalSettings.GetSaveslot();
		saveData.posX = ballController.gameObject.transform.position.x;
		saveData.posY = ballController.gameObject.transform.position.y;
		saveData.forceX = ballController.rb.velocity.x;
		saveData.forceY = ballController.rb.velocity.y;
		saveData.time = ballController.globalTimer;
		saveData.hits = ballController.hits;
		saveData.lariatCount = ballController.lariatCount;
		saveData.cannon = ballController.cannon;
		saveData.narration01 = narrationController.narration01;
		saveData.narration02 = narrationController.narration02;
		saveData.narration03 = narrationController.narration03;
		saveData.narration04 = narrationController.narration04;
		saveData.narration05 = narrationController.narration05;
		saveData.narration06 = narrationController.narration06;
		saveData.narration07 = narrationController.narration07;
		saveData.narration08 = narrationController.narration08;
		saveData.narrationGirl = narrationController.narrationGirl;
		saveData.narrationFall = narrationController.narrationFall;
		saveData.narrationJourney = narrationController.narrationJourney;
		saveData.finished = ballController.gameEnded;
		saveData.anime_animals = ballController.anime_animals;
		saveData.alvatross_castle = ballController.alvatross_castle;
		saveData.tower_of_swords = ballController.tower_of_swords;
		saveData.suicidal_emojis = ballController.suicidal_emojis;
		saveData.moon_officer = ballController.moon_officer;
		saveData.golfed_over_it = ballController.golfed_over_it;
		saveData.golfed_over_it_with_precission = ballController.golfed_over_it_with_precission;
		saveData.golfed_over_it_quick = ballController.golfed_over_it_quick;
		saveData.and_again = ballController.and_again;
		string text = string.Empty;
		for (int i = 0; i < narrationController.narrationQueue.Length; i++)
		{
			text = text + narrationController.narrationQueue[i] + "_";
		}
		saveData.narrationQueue = text;
		{
			saveData.Save(Path.Combine(savesPath, "save" + saveFile + ".xml"));
		}
	}

	public void QuickSave()
	{
		if (BallController.ballController != null && BallController.ballController.stopSaving)
		{
			return;
		}
		SaveData saveData = new SaveData();
		BallController ballController = BallController.ballController;
		saveFile = GlobalSettings.globalSettings.GetSaveslot();
		saveData.posX = ballController.gameObject.transform.position.x;
		saveData.posY = ballController.gameObject.transform.position.y;
		saveData.forceX = ballController.rb.velocity.x;
		saveData.forceY = ballController.rb.velocity.y;
		saveData.time = ballController.globalTimer;
		saveData.hits = ballController.hits;
		{
			saveData.Save(Path.Combine(savesPath, "save" + saveFile + ".xml"));
		}
	}

	public void Load()
	{
		saveFile = GlobalSettings.globalSettings.GetSaveslot();
		if (File.Exists(Path.Combine(savesPath, "save" + saveFile + ".xml")))
		{
			SaveData data = SaveData.Load(Path.Combine(savesPath, "save" + saveFile + ".xml"));
			if (MainMenuInputController.mainMenuInputController == null)
			{
				ParseLoadedData(data);
			}
			else
			{
				ParseLoadedDataMainMenu(data);
			}
		}
		else
		{
			ResetSave();
			Load();
		}
	}

	private void ParseLoadedData(SaveData data)
	{
		BallController ballController = BallController.ballController;
		NarrationController narrationController = NarrationController.narrationController;
		Vector3 position = new Vector3(data.posX, data.posY, ballController.gameObject.transform.position.z);
		ballController.gameObject.transform.position = position;
		Vector2 force = new Vector2(data.forceX, data.forceY);
		ballController.rb.AddForce(force, ForceMode2D.Impulse);
		ballController.globalTimer = data.time;
		ballController.hits = data.hits;
		ballController.lariatCount = data.lariatCount;
		ballController.cannon = data.cannon;
		if (data.cannon)
		{
			CanonController.cannonController.ShowShoot();
		}
		narrationController.narration01 = data.narration01;
		narrationController.narration02 = data.narration02;
		narrationController.narration03 = data.narration03;
		narrationController.narration04 = data.narration04;
		narrationController.narration05 = data.narration05;
		narrationController.narration06 = data.narration06;
		narrationController.narration07 = data.narration07;
		narrationController.narration08 = data.narration08;
		narrationController.narrationGirl = data.narrationGirl;
		narrationController.narrationFall = data.narrationFall;
		narrationController.narrationJourney = data.narrationJourney;
		ballController.anime_animals = data.anime_animals;
		ballController.alvatross_castle = data.alvatross_castle;
		ballController.tower_of_swords = data.tower_of_swords;
		ballController.suicidal_emojis = data.suicidal_emojis;
		ballController.moon_officer = data.moon_officer;
		ballController.golfed_over_it = data.golfed_over_it;
		ballController.golfed_over_it_with_precission = data.golfed_over_it_with_precission;
		ballController.golfed_over_it_quick = data.golfed_over_it_quick;
		ballController.and_again = data.and_again;
		if (data.narrationQueue != string.Empty)
		{
			string[] array = data.narrationQueue.Split('_');
			for (int i = 0; i < narrationController.narrationQueue.Length && !(array[i] == string.Empty); i++)
			{
				narrationController.narrationQueue[i] = int.Parse(array[i]);
			}
		}
		ballController.gameEnded = data.finished;
	}

	private void ParseLoadedDataMainMenu(SaveData data)
	{
		MainMenuInputController mainMenuInputController = MainMenuInputController.mainMenuInputController;
		mainMenuInputController.saveslotTime = data.time;
		mainMenuInputController.saveslotFinished = data.finished;
	}

	public void ResetSave()
	{
		SaveData saveData = new SaveData();
		saveData.posX = 0f;
		saveData.posY = 0f;
		saveData.forceX = 0f;
		saveData.forceY = 0f;
		saveData.time = 0f;
		saveData.hits = 0;
		saveData.lariatCount = 0;
		saveData.cannon = false;
		saveData.narration01 = false;
		saveData.narration02 = false;
		saveData.narration03 = false;
		saveData.narration04 = false;
		saveData.narration05 = false;
		saveData.narration06 = false;
		saveData.narration07 = false;
		saveData.narration08 = false;
		saveData.narrationGirl = false;
		saveData.narrationFall = false;
		saveData.narrationJourney = false;
		saveData.narrationQueue = string.Empty;
		saveData.finished = false;
		saveData.anime_animals = false;
		saveData.alvatross_castle = false;
		saveData.tower_of_swords = false;
		saveData.suicidal_emojis = false;
		saveData.moon_officer = false;
		saveData.golfed_over_it = false;
		saveData.golfed_over_it_with_precission = false;
		saveData.golfed_over_it_quick = false;
		saveData.and_again = false;
		{
			saveData.Save(Path.Combine(savesPath, "save" + saveFile + ".xml"));
		}
	}

	private string StringListToString(List<string> list)
	{
		string text = string.Empty;
		foreach (string item in list)
		{
			text = ((!(text == string.Empty)) ? (text + "," + item) : (text + item));
		}
		return text;
	}

	private string IntListToString(List<int> list)
	{
		string text = string.Empty;
		foreach (int item in list)
		{
			text = ((!(text == string.Empty)) ? (text + "," + item) : (text + item));
		}
		return text;
	}

	private List<string> LoadStringList(string line)
	{
		List<string> list = new List<string>();
		string[] array = line.Split(new string[1] { "," }, StringSplitOptions.None);
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text != string.Empty && text != null)
			{
				list.Add(text);
			}
		}
		return list;
	}

	private List<int> LoadIntList(string line)
	{
		List<int> list = new List<int>();
		string[] array = line.Split(new string[1] { "," }, StringSplitOptions.None);
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text != string.Empty && text != null)
			{
				int item = int.Parse(text);
				list.Add(item);
			}
		}
		return list;
	}

	private void StringToIntArray(int[] array, int length, string line)
	{
		if (!(line == string.Empty))
		{
			array = new int[length];
			string[] array2 = line.Split(new string[1] { "," }, StringSplitOptions.None);
			for (int i = 0; i < length; i++)
			{
				array[i] = int.Parse(array2[i]);
			}
		}
	}

	private string IntArrayToString(int[] array)
	{
		string text = string.Empty;
		for (int i = 0; i < array.Length; i++)
		{
			text = ((!(text == string.Empty)) ? (text + "," + array[i]) : (text + array[i]));
		}
		return text;
	}
}
