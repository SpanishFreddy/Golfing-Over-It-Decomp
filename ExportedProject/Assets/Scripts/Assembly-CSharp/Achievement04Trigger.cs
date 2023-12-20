using UnityEngine;

public class Achievement04Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.alvatross_castle)
		{
			BallController.ballController.alvatross_castle = true;
		}
	}
}
