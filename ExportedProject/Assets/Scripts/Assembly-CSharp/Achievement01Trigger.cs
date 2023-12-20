using UnityEngine;

public class Achievement01Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!BallController.ballController.over_the_tree)
		{
			BallController.ballController.over_the_tree = true;
		}
	}
}
