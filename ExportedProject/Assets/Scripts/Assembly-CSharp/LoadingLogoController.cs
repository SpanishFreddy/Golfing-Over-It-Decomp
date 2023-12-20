using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingLogoController : MonoBehaviour
{
	private AsyncOperation asyncLoad;

	private bool endTriggered;

	private void Start()
	{
		base.gameObject.GetComponent<Animator>().SetTrigger("start");
		asyncLoad = SceneManager.LoadSceneAsync("Title");
		asyncLoad.allowSceneActivation = false;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (asyncLoad.progress > 0.8f && !endTriggered)
		{
			endTriggered = true;
			base.gameObject.GetComponent<Animator>().SetTrigger("end");
		}
	}

	private void FinishLoading()
	{
		asyncLoad.allowSceneActivation = true;
	}
}
