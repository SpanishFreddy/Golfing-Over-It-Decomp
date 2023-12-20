using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
	public static BallController ballController;

	private bool cheats;

	public GameObject cursor;

	private float hitInterpolation;

	public Rigidbody2D rb;

	private Vector2 inputForce;

	private bool forceRegistered;

	private float originalStrength = 10f;

	private float strength;

	private float strengthTimer;

	public bool killInput = true;

	private bool ignoreInput = true;

	public AudioClip swing;

	public AudioClip bounce1;

	public AudioClip bounce2;

	public AudioClip bounce3;

	public AudioClip bounce4;

	public AudioClip bounce5;

	public AudioClip drone;

	public AudioClip lariat_magic;

	private float scale;

	public int hits;

	public float globalTimer;

	private float controlTimer;

	public Text txt;

	private bool resultsShown;

	private float lastSaveTime;

	private bool savedOnStop;

	private SpriteRenderer sr;

	private SpriteRenderer cursor_sr;

	private Vector3 lastStandingPos;

	private Vector3 lastBouncePos;

	public Sprite ball;

	public Sprite ball2;

	private float comboCooldownMultiplier = 1.1f;

	public GameObject lariatContainer;

	public GameObject lariat;

	public GameObject lariatPS;

	public int lariatCount;

	public bool cannon;

	public bool fadeEnded;

	public bool gameEnded;

	public bool stopSaving;

	private bool interrumptedCredits;

	private bool recallInitialEnableInput;

	public bool first_swing;

	public bool over_the_tree;

	public bool top_of_the_stairs;

	public bool anime_animals;

	public bool alvatross_castle;

	public bool tower_of_swords;

	public bool suicidal_emojis;

	public bool moon_officer;

	public bool golfed_over_it;

	public bool golfed_over_it_with_precission;

	public bool golfed_over_it_quick;

	public bool and_again;

	private void Start()
	{
		ballController = this;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		rb = base.gameObject.GetComponent<Rigidbody2D>();
		sr = base.gameObject.GetComponent<SpriteRenderer>();
		cursor_sr = cursor.GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		if (recallInitialEnableInput)
		{
			InitialEnableInput();
		}
		if (!fadeEnded)
		{
			return;
		}
		if (!gameEnded)
		{
			globalTimer += Time.deltaTime;
		}
		ManageInput();
		if (rb.velocity.x == 0f && rb.velocity.y == 0f && !killInput)
		{
			lastStandingPos = base.transform.position;
			comboCooldownMultiplier = 1.1f;
			ignoreInput = false;
			sr.color = Color.white;
			sr.sprite = ball;
			cursor_sr.color = Color.white;
			if (!savedOnStop)
			{
				savedOnStop = true;
				SaveLoad.saveLoad.Save();
			}
		}
		if (strength < originalStrength)
		{
			strength = Mathf.Lerp(0f, originalStrength, strengthTimer);
			strengthTimer += Time.deltaTime * comboCooldownMultiplier;
		}
		if (base.gameObject.transform.position.y > 752f && base.gameObject.transform.position.y < 753f && base.gameObject.transform.position.x > 164f && base.gameObject.transform.position.x < 165f && rb.velocity.x == 0f && rb.velocity.y == 0f)
		{
			if (!resultsShown)
			{
				ShowResults();
			}
			else if (interrumptedCredits)
			{
				interrumptedCredits = false;
				EndingCreditsController.endingCreditsController.MoveCameraAgain();
			}
		}
		CheckIfFall();
		ResetIfOut();
	}

	private void ManageInput()
	{
		if (cheats)
		{
			if (Input.GetKeyDown("r"))
			{
				base.transform.position = new Vector3(0f, 0f, 0f);
				globalTimer = 0f;
				hits = 0;
			}
			else if (Input.GetKeyDown("space"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = lastStandingPos;
			}
			else if (Input.GetKeyDown("1"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(107.23f, 6.09f, 0f);
			}
			else if (Input.GetKeyDown("2"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(167.66f, 38.38f, 0f);
			}
			else if (Input.GetKeyDown("3"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(124.69f, 81.54f, 0f);
			}
			else if (Input.GetKeyDown("4"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(101.37f, 130.34f, 0f);
			}
			else if (Input.GetKeyDown("5"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(118.16f, 303.11f, 0f);
			}
			else if (Input.GetKeyDown("6"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(38.75f, 329.4f, 0f);
			}
			else if (Input.GetKeyDown("7"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(14.94f, 396.67f, 0f);
			}
			else if (Input.GetKeyDown("8"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(102.5f, 535.9f, 0f);
			}
			else if (Input.GetKeyDown("9"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(-7.5f, 399.7f, 0f);
			}
			else if (Input.GetKeyDown("0"))
			{
				rb.velocity = Vector3.zero;
				base.transform.position = new Vector3(163.33f, 753.18f, 0f);
			}
		}
		if ((GlobalInput.globalInput.click || GlobalInput.globalInput.accept || GlobalInput.globalInput.cancel) && !killInput)
		{
			if (!cursor.activeSelf)
			{
				cursor.SetActive(true);
				Vector3 position = cursor.transform.position;
				Vector3 position2 = base.gameObject.transform.position;
				position2.z = position.z;
				cursor.transform.position = position2;
			}
			float num = GlobalSettings.globalSettings.cursorSentitivity;
			if (GlobalSettings.globalSettings.mobile)
			{
				num /= 3f;
			}
			float x = Input.GetAxis("MouseX") * num;
			float y = Input.GetAxis("MouseY") * num;
			if (GlobalInput.globalInput.usingController)
			{
				x = GlobalInput.globalInput.joystickAxis.x * num;
				y = GlobalInput.globalInput.joystickAxis.y * num;
			}
			cursor.transform.Translate(x, y, 0f);
			Vector3 position3 = cursor.transform.position;
			Vector3 position4 = base.transform.position;
			position4.z = position3.z;
			Vector3 vector = position3 - position4;
			vector = Vector3.ClampMagnitude(vector, 3f);
			position3 = position4 + vector;
			cursor.transform.position = position3;
			scale = 0.5f * Vector3.Distance(position3, position4) / (originalStrength / strength);
			cursor.transform.localScale = new Vector3(scale, scale, 1f);
		}
		else
		{
			if (!cursor.activeSelf || killInput)
			{
				return;
			}
			if (!forceRegistered && !ignoreInput)
			{
				float num2 = base.gameObject.transform.position.x - cursor.transform.position.x;
				float num3 = base.gameObject.transform.position.y - cursor.transform.position.y;
				float num4 = 0.3f;
				if (num2 < 0f)
				{
					num2 += num4;
					if (num2 > 0f)
					{
						num2 = 0f;
					}
				}
				else
				{
					num2 -= num4;
					if (num2 < 0f)
					{
						num2 = 0f;
					}
				}
				if (num3 < 0f)
				{
					num3 += num4;
					if (num3 > 0f)
					{
						num3 = 0f;
					}
				}
				else
				{
					num3 -= num4;
					if (num3 < 0f)
					{
						num3 = 0f;
					}
				}
				num2 *= 2.2f;
				num3 *= 2.2f;
				inputForce.x = num2 * strength;
				inputForce.y = num3 * strength;
				forceRegistered = true;
				comboCooldownMultiplier -= 0.1f;
				if (!gameEnded)
				{
					controlTimer = globalTimer;
				}
			}
			hitInterpolation += 20f * Time.deltaTime;
			Vector3 position5 = cursor.transform.position;
			Vector3 position6 = base.transform.position;
			position6.z = position5.z;
			position5 = Vector3.Lerp(position5, position6, hitInterpolation);
			cursor.transform.position = position5;
			if (!(hitInterpolation >= 1f))
			{
				return;
			}
			hitInterpolation = 0f;
			cursor.transform.position = position6;
			cursor.SetActive(false);
			if (ignoreInput)
			{
				return;
			}
			forceRegistered = false;
			if (rb.velocity.x > 0f || rb.velocity.x < 0f || rb.velocity.y > 0f || rb.velocity.y < 0f)
			{
				if (inputForce.x < 3f && inputForce.x > -3f)
				{
					if (inputForce.x > 0f)
					{
						inputForce.x = 3f;
					}
					else if (inputForce.x < 0f)
					{
						inputForce.x = -3f;
					}
					else if (rb.velocity.x > 0f)
					{
						inputForce.x = 3f;
					}
					else if (rb.velocity.x < 0f)
					{
						inputForce.x = -3f;
					}
				}
				if (inputForce.x < 10f && inputForce.x > -10f && inputForce.y < 20f && inputForce.y > -20f)
				{
					rb.velocity /= 1.5f;
				}
				else
				{
					rb.velocity = Vector3.zero;
				}
				rb.AddForce(inputForce, ForceMode2D.Impulse);
				PlaySwing();
			}
			else
			{
				rb.velocity = Vector3.zero;
				rb.AddForce(inputForce, ForceMode2D.Impulse);
				PlaySwing();
			}
			strength = 0f;
			strengthTimer = 0f;
			if (!gameEnded)
			{
				hits++;
			}
			if (!first_swing)
			{
				first_swing = true;
			}
			savedOnStop = false;
			if (gameEnded && inputForce.y != 0f && !CameraController.cameraController.enabled)
			{
				interrumptedCredits = true;
				CameraController.cameraController.enabled = true;
			}
		}
	}

	private void ResetIfOut()
	{
		if (base.transform.position.y < -100f)
		{
			base.transform.position = new Vector3(0f, 0f, base.transform.position.z);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (controlTimer == globalTimer || forceRegistered)
		{
			PlayBounce();
			return;
		}
		ignoreInput = true;
		sr.sprite = ball2;
		cursor_sr.color = Color.black;
		inputForce = Vector2.zero;
		PlayBounce();
		lastBouncePos = base.gameObject.transform.position;
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (rb.velocity.x < 0.01f && rb.velocity.x > -0.01f && rb.velocity.y < 0.01f && rb.velocity.y > -0.01f && !killInput)
		{
			rb.velocity = Vector2.zero;
		}
	}

	private void PlaySwing()
	{
		GlobalAudio.globalAudio.PlaySound(swing, Mathf.Pow(scale, 1.66f), 1.5f - scale / 2f, 0f);
	}

	private void PlayBounce()
	{
		if (fadeEnded)
		{
			AudioClip clip = bounce1;
			switch (Random.Range(1, 6))
			{
			case 1:
				clip = bounce1;
				break;
			case 2:
				clip = bounce2;
				break;
			case 3:
				clip = bounce3;
				break;
			case 4:
				clip = bounce4;
				break;
			case 5:
				clip = bounce5;
				break;
			}
			GlobalAudio.globalAudio.PlaySound(clip, Random.Range(0.75f, 1f), Random.Range(0.9f, 1.1f), 0f);
		}
	}

	private void ShowResults()
	{
		gameEnded = true;
		golfed_over_it = true;
		if (globalTimer < 900f)
		{
			golfed_over_it_quick = true;
		}
		if (hits < 300)
		{
			golfed_over_it_with_precission = true;
		}
		EndingCreditsController.endingCreditsController.StartCredits();
		SaveLoad.saveLoad.Save();
		stopSaving = true;
		resultsShown = true;
	}

	private void CheckIfFall()
	{
		if (!(base.gameObject.transform.position.y < lastBouncePos.y - 70f))
		{
			return;
		}
		if (!NarrationController.narrationController.narrationFall)
		{
			NarrationController.narrationController.AddOnlyIfEmptyQueue(10);
			lariatCount = 1;
		}
		else if (lariatCount > 5 && NarrationController.narrationController.narrationFall && !NarrationController.narrationController.narrationJourney)
		{
			NarrationController.narrationController.AddOnlyIfEmptyQueue(11);
			lariatCount = 1;
			if (!and_again)
			{
				and_again = true;
			}
		}
		else if (ignoreInput && NarrationController.narrationController.narrationFall && NarrationController.narrationController.narrationJourney)
		{
			lastBouncePos = base.gameObject.transform.position;
			lariatCount++;
			if (lariatCount > 5 && Random.Range(0, 20) < lariatCount)
			{
				ActivateLariat();
			}
		}
		else
		{
			lariatCount++;
		}
	}

	private void ActivateLariat()
	{
		killInput = true;
		lariatCount = 0;
		rb.gravityScale = 0f;
		rb.velocity = Vector2.zero;
		lariatContainer.transform.position = base.gameObject.transform.position;
		lariat.GetComponent<Animator>().SetTrigger("action");
		lariatPS.SetActive(true);
		GlobalAudio.globalAudio.PlaySound(drone, GlobalSettings.globalSettings.soundsVolume, 1f, 0f);
		Invoke("PlayMagic", 4f);
		Invoke("RecoverBall", 5f);
	}

	private void PlayMagic()
	{
		GlobalAudio.globalAudio.PlaySound(lariat_magic, GlobalSettings.globalSettings.soundsVolume, 1f, 0f);
	}

	private void RecoverBall()
	{
		lariat.GetComponent<Animator>().ResetTrigger("action");
		base.gameObject.transform.position = lastStandingPos;
		rb.gravityScale = 10f;
		lariatPS.SetActive(false);
		killInput = false;
	}

	public void InitialEnableInput()
	{
		if (rb.velocity.x == 0f && rb.velocity.y == 0f)
		{
			recallInitialEnableInput = false;
			killInput = false;
			ignoreInput = false;
			fadeEnded = true;
			base.gameObject.GetComponent<TrailRenderer>().enabled = true;
		}
		else
		{
			recallInitialEnableInput = true;
		}
	}

	public void DeactivateCursor()
	{
		cursor.transform.position = new Vector3(base.gameObject.transform.position.x, base.gameObject.transform.position.y, cursor.transform.position.z);
		cursor.SetActive(false);
	}

	public void ResetStrength()
	{
		strength = originalStrength;
	}
}
