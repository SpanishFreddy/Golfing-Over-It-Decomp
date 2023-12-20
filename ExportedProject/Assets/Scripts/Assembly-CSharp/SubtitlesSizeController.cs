using UnityEngine;

public class SubtitlesSizeController : MonoBehaviour
{
	private RectTransform subtitles;

	private Vector2 previousResolution;

	private void Start()
	{
		subtitles = base.gameObject.GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 vector = new Vector2(Screen.width, Screen.height);
		if (!(vector == previousResolution))
		{
			ResizeSubtitles();
			previousResolution = vector;
		}
	}

	private void ResizeSubtitles()
	{
		Vector2 vector = new Vector2(Screen.width, Screen.height);
		float num = vector.x / vector.y;
		subtitles.sizeDelta = new Vector2(700f * num, subtitles.sizeDelta.y);
	}
}
