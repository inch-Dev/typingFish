using UnityEngine;


public enum FishSize
{
	NULL = -1,
	SMALL,
	MEDIUM,
	LARGE
}

public class Fish : MonoBehaviour
{
	public FishData fishData;
}


[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData", order = 2)]
public class FishData: ScriptableObject
{
	[SerializeField] string fishName;
	[SerializeField] Sprite fishSprite;

	[SerializeField] FishSize fishSize;

	[HideInInspector] public bool isCaught = false;
}
