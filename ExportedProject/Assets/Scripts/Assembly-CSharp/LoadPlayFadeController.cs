using UnityEngine;

public class LoadPlayFadeController : MonoBehaviour
{
	private void Load()
	{
		SaveLoad.saveLoad.Load();
	}

	private void EnableInput()
	{
		BallController.ballController.InitialEnableInput();
		BallController.ballController.ResetStrength();
		InputController.inputController.killInput = false;
		base.gameObject.SetActive(false);
	}
}
