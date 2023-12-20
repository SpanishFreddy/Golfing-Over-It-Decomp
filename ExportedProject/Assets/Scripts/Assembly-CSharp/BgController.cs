using UnityEngine;

public class BgController : MonoBehaviour
{
	public GameObject mainCamera;

	private void Update()
	{
		Vector3 position = mainCamera.transform.position;
		Vector3 a = new Vector3(0f, 0f, 0f);
		base.transform.position = Vector3.Lerp(a, position, 0.2f);
	}
}
