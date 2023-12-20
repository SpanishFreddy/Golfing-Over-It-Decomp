using UnityEngine;

public class CursorContainerController : MonoBehaviour
{
	public GameObject ball;

	private void Update()
	{
		base.transform.position = ball.transform.position;
	}
}
