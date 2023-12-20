using UnityEngine;

public class CanonController : MonoBehaviour
{
	public static CanonController cannonController;

	public GameObject burnt;

	public GameObject girl;

	public AudioClip explosion;

	private void Start()
	{
		cannonController = this;
		if (BallController.ballController.cannon)
		{
			ShowShoot();
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!BallController.ballController.cannon)
		{
			ShootCannon();
		}
	}

	private void ShootCannon()
	{
		BallController.ballController.cannon = true;
		burnt.SetActive(true);
		girl.SetActive(false);
		GlobalAudio.globalAudio.PlaySound(explosion, GlobalSettings.globalSettings.soundsVolume, 1f, 0f);
	}

	public void ShowShoot()
	{
		burnt.SetActive(true);
		girl.SetActive(false);
	}

	public void HideGirl()
	{
		girl.SetActive(false);
	}
}
