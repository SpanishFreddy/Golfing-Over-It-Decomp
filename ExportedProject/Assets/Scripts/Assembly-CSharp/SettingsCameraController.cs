using System;
using UnityEngine;

public class SettingsCameraController : MonoBehaviour
{
	public Camera cam;

	private float previousAspectRatio = 1.777f;

	private Vector2 previousResolution;

	private void Start()
	{
		cam = base.gameObject.GetComponent<Camera>();
		AdjustCameraFOV();
	}

	private void Update()
	{
		AdjustCameraFOV();
	}

	private void AdjustCameraFOV()
	{
		Vector2 vector = new Vector2(Screen.width, Screen.height);
		if (!(vector == previousResolution))
		{
			previousResolution = vector;
			float num = vector.x / vector.y;
			if (num <= 1.7777778f)
			{
				float num2 = 91.49284f;
				float num3 = num2 * (float)Math.PI / 180f;
				float num4 = 2f * Mathf.Atan(Mathf.Tan(num3 / 2f) * vector.y / vector.x);
				float fieldOfView = num4 * 180f / (float)Math.PI;
				cam.fieldOfView = fieldOfView;
			}
		}
	}
}
