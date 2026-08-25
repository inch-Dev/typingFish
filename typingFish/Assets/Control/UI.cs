using UnityEngine;
public class UI : MonoBehaviour
{
	public string uiName;
	bool isActive = false;
	public CanvasGroup canvasGroup;
	public GameState activeStates;

	public void Toggle(bool isActive)
	{
		this.isActive = isActive;
		canvasGroup.interactable = isActive;
		canvasGroup.blocksRaycasts = isActive;
		canvasGroup.alpha = isActive ? 1 : 0;
	}

	public void Toggle(GameState state)
	{
		if (!activeStates.HasFlag(state))
			Toggle(false);

		else
			Toggle(true);
	}

	public void Toggle(GameState state, bool isActive)
	{
		if (!activeStates.HasFlag(state))
			return;
		Toggle(isActive);
	}
}