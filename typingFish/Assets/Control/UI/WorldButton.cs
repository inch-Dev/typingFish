using UnityEngine;

public class WorldButton : MonoBehaviour
{
	[SerializeField] int badButtonFunctionNum;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			Debug.Log("Doing something");
			switch (badButtonFunctionNum)
			{
				case 0:
					Quit();
					break;
				case 1:
					Start();
					break;
			}	

		}
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Start()
	{
		if (GameManager.instance != null)
		{
			GameManager.instance.SetState(GameState.CASTING);
			Debug.Log("Setting start");
		}
	}
}
