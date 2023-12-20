using UnityEngine;

public class Achievement05Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.tower_of_swords)
		{
			BallController.ballController.tower_of_swords = true;
		}
	}
}
