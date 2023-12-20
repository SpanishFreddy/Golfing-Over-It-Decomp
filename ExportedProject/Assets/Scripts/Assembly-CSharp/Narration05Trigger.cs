using UnityEngine;

public class Narration05Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration05)
		{
			NarrationController.narrationController.AddToQueue(5);
			NarrationController.narrationController.narration05 = true;
		}
	}
}
