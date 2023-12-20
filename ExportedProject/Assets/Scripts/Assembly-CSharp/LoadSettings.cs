using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSettings : MonoBehaviour
{
	private void Start()
	{
		SceneManager.LoadSceneAsync("Settings", LoadSceneMode.Additive);
	}
}
