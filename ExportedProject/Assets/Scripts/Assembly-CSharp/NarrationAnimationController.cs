using UnityEngine;

public class NarrationAnimationController : MonoBehaviour
{
	public AudioClip gymnopedie1;

	public AudioClip gymnopedie2;

	public AudioClip gymnopedie3;

	public AudioClip narration_01;

	public AudioClip narration_02;

	public AudioClip narration_03;

	public AudioClip narration_04;

	public AudioClip narration_05;

	public AudioClip narration_06;

	public AudioClip narration_07;

	public AudioClip narration_08;

	public AudioClip narration_girl;

	public AudioClip narration_fall;

	public AudioClip narration_journey;

	private void PlayMusic01()
	{
		GlobalAudio.globalAudio.PlayMusic(gymnopedie1, 1f, 1f, 0f);
	}

	private void PlayMusic02()
	{
		GlobalAudio.globalAudio.PlayMusic(gymnopedie2, 1f, 1f, 0f);
	}

	private void PlayMusic03()
	{
		GlobalAudio.globalAudio.PlayMusic(gymnopedie3, 1f, 1f, 0f);
	}

	private void Fademusic()
	{
		if (!(BallController.ballController.gameObject.transform.position.y > 752f) || !(BallController.ballController.gameObject.transform.position.y < 753f) || !(BallController.ballController.gameObject.transform.position.x > 164f) || !(BallController.ballController.gameObject.transform.position.x < 165f))
		{
			GlobalAudio.globalAudio.ActivateFadeMusic();
		}
	}

	private void PlayNarration01()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_01, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration02()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_02, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration03()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_03, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration04()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_04, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration05()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_05, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration06()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_06, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration07()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_07, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarration08()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_08, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarrationGirl()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_girl, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarrationFall()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_fall, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void PlayNarrationJourney()
	{
		GlobalAudio.globalAudio.PlayNarration(narration_journey, GlobalSettings.globalSettings.narrationVolume, 1f, 0f);
	}

	private void End()
	{
		NarrationController.narrationController.PushQueue();
		NarrationController.narrationController.narrationPlaying = false;
		SaveLoad.saveLoad.Save();
	}

	private void DoNothig()
	{
	}
}
