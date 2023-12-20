using UnityEngine;

public class Narration04Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration04)
		{
			NarrationController.narrationController.AddToQueue(4);
			NarrationController.narrationController.narration04 = true;
		}
	}
}
