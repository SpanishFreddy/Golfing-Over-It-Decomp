using UnityEngine;

public class GeometryResetController : MonoBehaviour
{
	public GameObject geometry2_1;

	public GameObject geometry2_2;

	public GameObject geometry2_3;

	public GameObject geometry2_4;

	public GameObject geometry2_5;

	public GameObject geometry2_6;

	public GameObject geometry2_7;

	private GeometryController geometryController2_1;

	private GeometryController geometryController2_2;

	private GeometryController geometryController2_3;

	private GeometryController geometryController2_4;

	private GeometryController geometryController2_5;

	private GeometryController geometryController2_6;

	private GeometryController geometryController2_7;

	private bool alreadyResetted;

	private void Start()
	{
		geometryController2_1 = geometry2_1.GetComponent<GeometryController>();
		geometryController2_2 = geometry2_2.GetComponent<GeometryController>();
		geometryController2_3 = geometry2_3.GetComponent<GeometryController>();
		geometryController2_4 = geometry2_4.GetComponent<GeometryController>();
		geometryController2_5 = geometry2_5.GetComponent<GeometryController>();
		geometryController2_6 = geometry2_6.GetComponent<GeometryController>();
		geometryController2_7 = geometry2_7.GetComponent<GeometryController>();
	}

	private void Update()
	{
		if ((base.gameObject.transform.position.y < 540f || base.gameObject.transform.position.x > 100f) && !alreadyResetted)
		{
			ForceReset();
		}
		else
		{
			alreadyResetted = false;
		}
	}

	private void ForceReset()
	{
		alreadyResetted = true;
		geometryController2_1.ForceReset();
		geometryController2_2.ForceReset();
		geometryController2_3.ForceReset();
		geometryController2_4.ForceReset();
		geometryController2_5.ForceReset();
		geometryController2_6.ForceReset();
		geometryController2_7.ForceReset();
	}
}
