using UnityEngine;

public class GlobalInput : MonoBehaviour
{
	public static GlobalInput globalInput;

	public bool up;

	public bool down;

	public bool left;

	public bool right;

	public bool upDown;

	public bool downDown;

	public bool leftDown;

	public bool rightDown;

	public bool accept;

	public bool acceptDown;

	public bool cancel;

	public bool cancelDown;

	public bool exit;

	public bool exitDown;

	public bool start;

	public bool startDown;

	public bool click;

	public bool clickDown;

	public bool rightClick;

	public bool rightClickDown;

	public Vector2 joystickAxis = Vector2.zero;

	private bool controllerActivated;

	public bool usingController;

	public bool stepped;

	private float timePressed;

	public bool gameInputActive = true;

	private void Start()
	{
		globalInput = this;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightAlt) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.AltGr) || Input.GetKeyDown(KeyCode.Delete))
		{
			SaveLoad.saveLoad.QuickSave();
		}
		if (Input.GetAxisRaw("DownArrow") == 1f || Input.GetAxisRaw("DownWASD") == 1f || ((Input.GetAxisRaw("VerticalStick1") == 1f || Input.GetAxisRaw("VerticalStick2") == 1f || Input.GetAxisRaw("VerticalDpad") == 1f) && controllerActivated))
		{
			if (!down)
			{
				downDown = true;
			}
			else
			{
				downDown = false;
			}
			down = true;
		}
		else
		{
			down = false;
			downDown = false;
		}
		if (Input.GetAxisRaw("DownArrow") == 1f || Input.GetAxisRaw("DownWASD") == 1f || ((Input.GetAxisRaw("VerticalStick1") == 1f || Input.GetAxisRaw("VerticalStick2") == 1f || Input.GetAxisRaw("VerticalDpad") == 1f) && controllerActivated))
		{
			if (!down)
			{
				downDown = true;
			}
			else
			{
				downDown = false;
			}
			down = true;
		}
		else
		{
			down = false;
			downDown = false;
		}
		if (Input.GetAxisRaw("UpArrow") == 1f || Input.GetAxisRaw("UpWASD") == 1f || ((Input.GetAxisRaw("VerticalStick1") == -1f || Input.GetAxisRaw("VerticalStick2") == -1f || Input.GetAxisRaw("VerticalDpad") == -1f) && controllerActivated))
		{
			if (!up)
			{
				upDown = true;
			}
			else
			{
				upDown = false;
			}
			up = true;
		}
		else
		{
			up = false;
			upDown = false;
		}
		if (Input.GetAxisRaw("RightArrow") == 1f || Input.GetAxisRaw("RightWASD") == 1f || ((Input.GetAxisRaw("HorizontalStick1") == 1f || Input.GetAxisRaw("HorizontalStick2") == 1f || Input.GetAxisRaw("HorizontalDpad") == 1f) && controllerActivated))
		{
			if (!right)
			{
				rightDown = true;
			}
			else
			{
				rightDown = false;
			}
			right = true;
		}
		else
		{
			right = false;
			rightDown = false;
		}
		if (Input.GetAxisRaw("LeftArrow") == 1f || Input.GetAxisRaw("LeftWASD") == 1f || ((Input.GetAxisRaw("HorizontalStick1") == -1f || Input.GetAxisRaw("HorizontalStick2") == -1f || Input.GetAxisRaw("HorizontalDpad") == -1f) && controllerActivated))
		{
			if (!left)
			{
				leftDown = true;
			}
			else
			{
				leftDown = false;
			}
			left = true;
		}
		else
		{
			left = false;
			leftDown = false;
		}
		if (Input.GetAxisRaw("Accept") == 1f || Input.GetAxisRaw("Enter") == 1f || Input.GetAxisRaw("LeftTrigger") == 1f || Input.GetAxisRaw("LeftBumper") == 1f || Input.GetAxisRaw("RightTrigger") == 1f || Input.GetAxisRaw("RightBumper") == 1f)
		{
			if (!accept)
			{
				acceptDown = true;
			}
			else
			{
				acceptDown = false;
			}
			accept = true;
			if (Input.GetAxisRaw("Accept") == 1f || Input.GetAxisRaw("LeftTrigger") == 1f || Input.GetAxisRaw("LeftBumper") == 1f || Input.GetAxisRaw("RightTrigger") == 1f || Input.GetAxisRaw("RightBumper") == 1f)
			{
				controllerActivated = true;
				usingController = true;
			}
			else
			{
				usingController = false;
			}
		}
		else
		{
			accept = false;
			acceptDown = false;
		}
		if (Input.GetAxisRaw("Cancel") == 1f)
		{
			if (!cancel)
			{
				cancelDown = true;
			}
			else
			{
				cancelDown = false;
			}
			cancel = true;
			controllerActivated = true;
		}
		else
		{
			cancel = false;
			cancelDown = false;
		}
		if (Input.GetAxisRaw("Esc") == 1f)
		{
			if (!exit)
			{
				exitDown = true;
			}
			else
			{
				exitDown = false;
			}
			exit = true;
		}
		else
		{
			exit = false;
			exitDown = false;
		}
		if (Input.GetAxisRaw("Start") == 1f)
		{
			if (!start)
			{
				startDown = true;
			}
			else
			{
				startDown = false;
			}
			start = true;
			controllerActivated = true;
		}
		else
		{
			start = false;
			startDown = false;
		}
		if (Input.GetAxisRaw("MouseClick") == 1f)
		{
			if (!click)
			{
				clickDown = true;
			}
			else
			{
				clickDown = false;
			}
			click = true;
		}
		else
		{
			click = false;
			clickDown = false;
		}
		if (Input.GetAxisRaw("MouseRightClick") == 1f)
		{
			if (!rightClick)
			{
				rightClickDown = true;
			}
			else
			{
				rightClickDown = false;
			}
			rightClick = true;
		}
		else
		{
			rightClick = false;
			rightClickDown = false;
		}
		if (Input.GetAxis("HorizontalStick1") > 0.1f || Input.GetAxis("HorizontalStick1") < -0.1f)
		{
			joystickAxis.x = Input.GetAxis("HorizontalStick1");
		}
		else if (Input.GetAxis("HorizontalStick2") > 0.1f || Input.GetAxis("HorizontalStick2") < -0.1f)
		{
			joystickAxis.x = Input.GetAxis("HorizontalStick2");
		}
		else
		{
			joystickAxis.x = 0f;
		}
		if (Input.GetAxis("VerticalStick1") > 0.1f || Input.GetAxis("VerticalStick1") < -0.1f)
		{
			joystickAxis.y = Input.GetAxis("VerticalStick1") * -1f;
		}
		else if (Input.GetAxis("VerticalStick2") > 0.1f || Input.GetAxis("VerticalStick2") < -0.1f)
		{
			joystickAxis.y = Input.GetAxis("VerticalStick2") * -1f;
		}
		else
		{
			joystickAxis.y = 0f;
		}
		if (up || down || left || right)
		{
			if (!stepped && timePressed == 0f)
			{
				stepped = true;
			}
			else if (timePressed >= 0.25f)
			{
				stepped = true;
				timePressed = 0f;
			}
			else
			{
				stepped = false;
			}
			timePressed += Time.unscaledDeltaTime;
		}
		else
		{
			timePressed = 0f;
			stepped = false;
		}
	}
}
