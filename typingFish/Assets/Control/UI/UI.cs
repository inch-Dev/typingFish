using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
public class UI : MonoBehaviour, IStateable
{
	public void HandleState()
	{
		GameState checkState = GameManager.instance.GetState();
		bool matchState = false;

		foreach (GameState gameState in activeGameStates)
		{
			if(gameState == checkState)
			{
				Toggle(true);
				matchState = true;
			}
		}

		if (!matchState)
			Toggle(false);
	}	
	protected bool isActive = false;
	protected CanvasGroup canvasGroup;
	[SerializeField] List<GameState> activeGameStates = new List<GameState>();

	public void Toggle(bool isEnabled)
	{
		isActive = isEnabled;
		canvasGroup.interactable = isEnabled;
		canvasGroup.blocksRaycasts = isEnabled;
		canvasGroup.alpha = isEnabled ? 1 : 0;
	}

    private void Awake()
    {
		canvasGroup = GetComponent<CanvasGroup>();
    }

}