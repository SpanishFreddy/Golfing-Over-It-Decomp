using UnityEngine;

public class Achievement07Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.moon_officer)
		{
			BallController.ballController.moon_officer = true;
		}
	}
}
