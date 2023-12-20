using UnityEngine;

public class HardPathMusicTrigger : MonoBehaviour
{
	public AudioClip music;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(GlobalAudio.globalAudio.IsMusicPlaying());
		if (!GlobalAudio.globalAudio.IsMusicPlaying())
		{
			GlobalAudio.globalAudio.PlayMusic(music, 1f, 1f, 0f);
		}
	}
}
