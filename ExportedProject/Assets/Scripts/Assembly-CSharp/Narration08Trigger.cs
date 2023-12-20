using UnityEngine;

public class Narration08Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration08)
		{
			NarrationController.narrationController.AddToQueue(8);
			NarrationController.narrationController.narration08 = true;
		}
	}
}
