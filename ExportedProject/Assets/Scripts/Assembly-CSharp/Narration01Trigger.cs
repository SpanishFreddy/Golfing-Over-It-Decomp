using UnityEngine;

public class Narration01Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration01)
		{
			NarrationController.narrationController.AddToQueue(1);
			NarrationController.narrationController.narration01 = true;
		}
	}
}
