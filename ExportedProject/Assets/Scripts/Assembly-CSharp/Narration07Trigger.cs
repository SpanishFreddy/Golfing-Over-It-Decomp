using UnityEngine;

public class Narration07Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration07)
		{
			NarrationController.narrationController.AddToQueue(7);
			NarrationController.narrationController.narration07 = true;
		}
	}
}
