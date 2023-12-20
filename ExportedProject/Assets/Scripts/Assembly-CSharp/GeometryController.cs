using UnityEngine;

public class GeometryController : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		animator.SetTrigger("fade");
		Invoke("ResetFadeTrigger", 1f);
	}

	private void ResetFadeTrigger()
	{
		animator.ResetTrigger("fade");
	}

	public void ForceReset()
	{
		animator.SetTrigger("forceReset");
		Invoke("ResetForceTrigger", 0.5f);
	}

	private void ResetForceTrigger()
	{
		animator.ResetTrigger("forceReset");
	}
}
