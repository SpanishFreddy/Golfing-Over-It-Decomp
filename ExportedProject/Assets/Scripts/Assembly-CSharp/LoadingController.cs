using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
	private AsyncOperation asyncLoad;

	private void Start()
	{
		SceneManager.UnloadSceneAsync("Title");
		asyncLoad = SceneManager.LoadSceneAsync("Scene", LoadSceneMode.Additive);
		asyncLoad.allowSceneActivation = false;
		Cursor.visible = false;
	}

	private void Update()
	{
		if (asyncLoad.isDone)
		{
			SceneManager.UnloadSceneAsync("Loading");
		}
	}

	private void FinishLoading()
	{
		asyncLoad.allowSceneActivation = true;
	}
}
