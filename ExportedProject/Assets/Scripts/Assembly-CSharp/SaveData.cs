using System;
using System.IO;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("SaveData")]
public class SaveData
{
	public float posX;

	public float posY;

	public float forceX;

	public float forceY;

	public float time;

	public int hits;

	public int lariatCount;

	public bool cannon;

	public bool narration01;

	public bool narration02;

	public bool narration03;

	public bool narration04;

	public bool narration05;

	public bool narration06;

	public bool narration07;

	public bool narration08;

	public bool narrationGirl;

	public bool narrationFall;

	public bool narrationJourney;

	public string narrationQueue;

	public bool finished;

	public bool first_swing;

	public bool over_the_tree;

	public bool top_of_the_stairs;

	public bool anime_animals;

	public bool alvatross_castle;

	public bool tower_of_swords;

	public bool suicidal_emojis;

	public bool moon_officer;

	public bool golfed_over_it;

	public bool golfed_over_it_with_precission;

	public bool golfed_over_it_quick;

	public bool and_again;

	public void Save(string path)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
		using (StreamWriter textWriter = new StreamWriter(path))
		{
			xmlSerializer.Serialize(textWriter, this);
		}
	}

	public static SaveData Load(string path)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(SaveData));
		using (StreamReader textReader = new StreamReader(path))
		{
			return xmlSerializer.Deserialize(textReader) as SaveData;
		}
	}
}
