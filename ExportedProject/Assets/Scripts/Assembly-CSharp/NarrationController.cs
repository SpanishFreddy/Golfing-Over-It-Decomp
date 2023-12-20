using UnityEngine;

public class NarrationController : MonoBehaviour
{
	public static NarrationController narrationController;

	public int[] narrationQueue;

	public bool narration01;

	public bool narration02;

	public bool narration03;

	public bool narration04;

	public bool narration05;

	public bool narration06;

	public bool narration07;

	public bool narration08;

	public bool narrationGirl;

	public bool narrationFall;

	public bool narrationJourney;

	private float checkQueueCooldown;

	public bool narrationPlaying;

	private void Start()
	{
		narrationController = this;
		narrationQueue = new int[10];
	}

	private void Update()
	{
		if (!narrationPlaying && checkQueueCooldown > 1f)
		{
			checkQueueCooldown = 0f;
			CheckQueue();
		}
		checkQueueCooldown += Time.deltaTime;
	}

	private void CheckQueue()
	{
		int num = narrationQueue[0];
		if (num != 0)
		{
			ActivateNarration(num);
		}
	}

	public void PushQueue()
	{
		int num = narrationQueue[0];
		if (num > 0)
		{
			for (int i = 1; i < narrationQueue.Length; i++)
			{
				narrationQueue[i - 1] = narrationQueue[i];
			}
			narrationQueue[narrationQueue.Length - 1] = 0;
		}
	}

	private void ActivateNarration(int narration)
	{
		switch (narration)
		{
		case 1:
			base.gameObject.GetComponent<Animator>().SetTrigger("01");
			narrationPlaying = true;
			break;
		case 2:
			base.gameObject.GetComponent<Animator>().SetTrigger("02");
			narrationPlaying = true;
			break;
		case 3:
			base.gameObject.GetComponent<Animator>().SetTrigger("03");
			narrationPlaying = true;
			break;
		case 4:
			base.gameObject.GetComponent<Animator>().SetTrigger("04");
			narrationPlaying = true;
			break;
		case 5:
			base.gameObject.GetComponent<Animator>().SetTrigger("05");
			narrationPlaying = true;
			break;
		case 6:
			base.gameObject.GetComponent<Animator>().SetTrigger("06");
			narrationPlaying = true;
			break;
		case 7:
			base.gameObject.GetComponent<Animator>().SetTrigger("07");
			narrationPlaying = true;
			break;
		case 8:
			base.gameObject.GetComponent<Animator>().SetTrigger("08");
			narrationPlaying = true;
			break;
		case 9:
			base.gameObject.GetComponent<Animator>().SetTrigger("girl");
			narrationPlaying = true;
			break;
		case 10:
			base.gameObject.GetComponent<Animator>().SetTrigger("fall");
			narrationPlaying = true;
			break;
		case 11:
			base.gameObject.GetComponent<Animator>().SetTrigger("journey");
			narrationPlaying = true;
			break;
		}
		SaveLoad.saveLoad.Save();
	}

	public void AddToQueue(int n)
	{
		for (int i = 0; i < narrationQueue.Length && narrationQueue[i] != n; i++)
		{
			if (narrationQueue[i] <= 0)
			{
				narrationQueue[i] = n;
				break;
			}
		}
	}

	public void AddOnlyIfEmptyQueue(int n)
	{
		if (narrationQueue[0] <= 0)
		{
			narrationQueue[0] = n;
			switch (n)
			{
			case 10:
				narrationFall = true;
				break;
			case 11:
				narrationJourney = true;
				break;
			}
		}
	}

	public void ClearQueue()
	{
		narrationQueue = new int[10];
	}
}
