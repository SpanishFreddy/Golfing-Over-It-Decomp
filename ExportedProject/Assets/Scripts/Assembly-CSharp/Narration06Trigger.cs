using UnityEngine;

public class Narration06Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration06)
		{
			NarrationController.narrationController.AddToQueue(6);
			NarrationController.narrationController.narration06 = true;
		}
	}
}
