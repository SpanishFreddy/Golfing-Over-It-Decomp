using UnityEngine;

public class NarrationGirlTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narrationGirl)
		{
			NarrationController.narrationController.AddOnlyIfEmptyQueue(9);
			NarrationController.narrationController.narrationGirl = true;
		}
	}
}
