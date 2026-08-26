using UnityEngine;

[CreateAssetMenu(fileName = "FishData", menuName = "ScriptableObjects/FishData", order = 2)]
public class FishData : ScriptableObject
{
	[SerializeField] public string fishName;
	[SerializeField] public Sprite fishSprite;

	[SerializeField] public FishSize fishSize;

	[HideInInspector] public bool isCaught = false;
}
