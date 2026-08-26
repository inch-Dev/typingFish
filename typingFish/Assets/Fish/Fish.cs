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
	public Rigidbody2D rb;
	public FishData fishData;

}
