using UnityEngine;

public class MainMenuBallController : MonoBehaviour
{
	public AudioClip bounce1;

	public AudioClip bounce2;

	public AudioClip bounce3;

	public AudioClip bounce4;

	public AudioClip bounce5;

	private float vol = 1f;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		PlayBounce();
	}

	private void PlayBounce()
	{
		AudioClip clip = bounce1;
		switch (Random.Range(1, 6))
		{
		case 1:
			clip = bounce1;
			break;
		case 2:
			clip = bounce2;
			break;
		case 3:
			clip = bounce3;
			break;
		case 4:
			clip = bounce4;
			break;
		case 5:
			clip = bounce5;
			break;
		}
		GlobalAudio.globalAudio.PlaySound(clip, vol, 1f, 0f);
		vol -= 0.15f;
	}
}
