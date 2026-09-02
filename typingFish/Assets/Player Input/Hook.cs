using UnityEngine;

public class Hook : MonoBehaviour, IStateable
{
	public void HandleState()
	{
		switch(GameManager.instance.GetState())
		{
			case GameState.FISHING:
				ToggleHook(true);
				isFollowing = true;
				MoveToFishPosition();
				break;
			case GameState.TYPING:
				ToggleHook(false);
				isFollowing = false;
				break;
		}
	}
	[SerializeField] Vector3 fishingResetPosition;
	[SerializeField] Vector2 horizontalRangeClamp;
	[SerializeField] float verticalMoveSpeed;
	[SerializeField] float horizontalMoveSpeed;

	bool isFollowing = false;

	Rigidbody2D rb;
	CircleCollider2D circleCollider;

	SpriteRenderer spriteRenderer;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		if(isFollowing)
			FollowMouse();
	}

    void FollowMouse()
    {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
		Vector2 newPosition = Vector2.Lerp(transform.position, mousePosition, horizontalMoveSpeed);
		transform.position = new Vector2(Mathf.Clamp(newPosition.x, horizontalRangeClamp.x, horizontalRangeClamp.y), transform.position.y - (verticalMoveSpeed * Time.deltaTime));

		//Apply ForceDown
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.GetComponentInParent<Fish>())
		{
			GameManager.instance.SetState(GameState.TYPING);
			FishManager.instance.SetCatchingFish(collision.gameObject.GetComponentInParent<Fish>());
		}
	}

	void ToggleHook(bool isOn)
	{
		circleCollider.enabled = isOn;
		rb.simulated = isOn;
		spriteRenderer.enabled = isOn;
	}

	void MoveToFishPosition()
	{
		//Debug.Log("Resetting Mouse");
		transform.position = fishingResetPosition;
	}
}
