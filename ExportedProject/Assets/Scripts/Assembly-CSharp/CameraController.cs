using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController cameraController;

	private Camera cam;

	public GameObject ball;

	private Vector2 previousResolution;

	private void Start()
	{
		cameraController = this;
		cam = base.gameObject.GetComponent<Camera>();
		AdjustCameraFOV();
	}

	private void Update()
	{
		FollowBall();
		AdjustCameraFOV();
	}

	private void FollowBall()
	{
		Vector3 position = ball.transform.position;
		position.z = base.transform.position.z;
		base.transform.position = position;
	}

	private void AdjustCameraFOV()
	{
		Vector2 vector = new Vector2(Screen.width, Screen.height);
		if (!(vector == previousResolution))
		{
			previousResolution = vector;
			float num = vector.x / vector.y;
			if (num < 1.7777778f)
			{
				float num2 = 90f;
				float num3 = num2 * (float)Math.PI / 180f;
				float num4 = 2f * Mathf.Atan(Mathf.Tan(num3 / 2f) * vector.y / vector.x);
				float fieldOfView = num4 * 180f / (float)Math.PI;
				cam.fieldOfView = fieldOfView;
			}
			else
			{
				cam.fieldOfView = 60f;
			}
		}
	}
}
