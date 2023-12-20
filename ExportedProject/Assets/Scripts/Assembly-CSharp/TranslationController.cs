using System.Collections.Generic;
using UnityEngine;

public class TranslationController : MonoBehaviour
{
	public static TranslationController translationController;

	public TextAsset textAsset;

	public Dictionary<string, TextString> textStrings;

	private TextString textString;

	public bool textLoaded;

	private void Start()
	{
		translationController = this;
		LoadText();
	}

	private void LoadText()
	{
		string text = textAsset.text;
		string[] array = text.Split("\n"[0]);
		textStrings = new Dictionary<string, TextString>();
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].Contains("*"))
			{
				textString = new TextString();
				string[] array2 = array[i].Split("*"[0]);
				if (array2[0] != string.Empty)
				{
					textString.en = array2[2].Replace("---", "\u00a0").Trim();
					textString.es = array2[3].Replace("---", "\u00a0").Trim();
					textStrings.Add(array2[0], textString);
				}
			}
		}
		textLoaded = true;
	}
}
