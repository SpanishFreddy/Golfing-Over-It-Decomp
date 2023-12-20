using UnityEngine;

public class Achievement03Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.anime_animals)
		{
			BallController.ballController.anime_animals = true;
		}
	}
}
