using UnityEngine;

public class Achievement02Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.top_of_the_stairs)
		{
			BallController.ballController.top_of_the_stairs = true;
		}
	}
}
