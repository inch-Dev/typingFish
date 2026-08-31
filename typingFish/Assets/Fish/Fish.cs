using UnityEngine;


public enum FishSize
{
	NULL = -1,
	SMALL,
	MEDIUM,
	LARGE
}

public class Fish : MonoBehaviour, IStateable
{
    public CapsuleCollider2D collider;
	public FishData fishData;

	public void HandleState()
	{
        switch (GameManager.instance.GetState())
        {
            case GameState.TYPING:
                //Debug.Log("Toggle off simulation");
                collider.enabled = false;

                break;
            case GameState.FISHING:
                collider.enabled = true;
                break;

        }
    }

    private void Start()
    {
        collider = GetComponentInChildren<CapsuleCollider2D>();
        //Debug.Log($"Getting:{GetComponentInChildren<CapsuleCollider2D>()}");
    }

}
