using UnityEngine;

public class Narration02Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration02)
		{
			NarrationController.narrationController.AddToQueue(2);
			NarrationController.narrationController.narration02 = true;
		}
	}
}
