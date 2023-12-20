using UnityEngine;

public class Achievement06Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.suicidal_emojis)
		{
			BallController.ballController.suicidal_emojis = true;
		}
	}
}
