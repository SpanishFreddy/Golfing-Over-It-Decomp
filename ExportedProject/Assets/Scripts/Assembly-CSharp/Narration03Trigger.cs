using UnityEngine;

public class Narration03Trigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!NarrationController.narrationController.narration03)
		{
			NarrationController.narrationController.AddToQueue(3);
			NarrationController.narrationController.narration03 = true;
		}
	}
}
