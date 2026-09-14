using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Hook : MonoBehaviour, IStateable
{
	[HideInInspector] public static Hook instance;
	public void HandleState()
	{
		switch(GameManager.instance.GetState())
		{
			case GameState.START:
				Toggle(true);
				break;
			case GameState.CASTING:
				Toggle(true);
				isFollowingMouse = false;

				if(!wasCasting)
                    MoveToCastPosition();

                wasCasting = true;
				isCasting = true;
				
				break;
			case GameState.FISHING:
				isFollowingMouse = true;
				isCasting = false;
				castTimeElapsed = 0f;
				ToggleLogic(true);
				break;
			case GameState.TYPING:
			case GameState.PAUSED:
			case GameState.SESSION_OVER:
				isCasting = false;
				ToggleLogic(false);
				break;
		}
	}
	[Header("Casting")]
	[SerializeField] Vector3 castingResetPosition;
	[SerializeField] float castingTime;
	float castTimeElapsed = 0f;
	[SerializeField] float castingSpeed;

	[Header("Fishing")]
	[SerializeField] Vector2 horizontalRangeClamp;
	[SerializeField] float horizontalMoveSpeed;


	bool isCasting = false;
	bool wasCasting = false;
	bool isFollowingMouse = false;

	Rigidbody2D rb;
	CircleCollider2D circleCollider;

	[SerializeField] SpriteRenderer spriteRenderer;

	private void Start()
	{
		if (instance == null)
			instance = this;

		rb = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		if(isCasting)
		{
			castTimeElapsed += Time.fixedDeltaTime;
			if (castTimeElapsed >= castingTime)
			{
				isCasting = false;
				castTimeElapsed = 0f;
				wasCasting = false;
				GameManager.instance.SetState(GameState.FISHING);
			}

			else
				CastMove();
		}

		if(isFollowingMouse)
			FollowMouse();
	}

    void FollowMouse()
    {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
		Vector2 newPosition = Vector2.Lerp(transform.position, mousePosition, horizontalMoveSpeed);
		transform.position = new Vector2(Mathf.Clamp(newPosition.x, horizontalRangeClamp.x, horizontalRangeClamp.y), transform.position.y);
	}
	void Toggle(bool isOn)
	{
		spriteRenderer.enabled = isOn;
		isFollowingMouse = isOn;
	}

	void ToggleLogic(bool isOn)
	{
        circleCollider.enabled = isOn;
        rb.simulated = isOn;
		isFollowingMouse = isOn;
    }

	void MoveToCastPosition()
	{
		transform.position = castingResetPosition;
	}

	void CastMove()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + (castingSpeed * Time.fixedDeltaTime), 0f);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Fish>())
        {
            GameManager.instance.SetState(GameState.TYPING);
            collision.gameObject.GetComponentInParent<Fish>().SetMove(false);
            FishManager.instance.SetCatchingFish(collision.gameObject.GetComponentInParent<Fish>());
        }
    }
}
