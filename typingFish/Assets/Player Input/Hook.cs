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
				isMoving = true;

				if(!wasCasting)
                    MoveToCastPosition();

                wasCasting = true;
				isCasting = true;
				
				break;
			case GameState.FISHING:
				isMoving = true;
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

	[Header("Visuals")]
	[SerializeField] Vector3 hookLineOriginPosition;
	[SerializeField] Vector3 hookLineConnectPositionOffset;


	bool isCasting = false;
	bool wasCasting = false;
	bool isMoving = false;

	Rigidbody2D rb;
	CircleCollider2D circleCollider;
	LineRenderer lineRenderer;
	[SerializeField] SpriteRenderer spriteRenderer;

	private void Start()
	{
		if (instance == null)
			instance = this;

		rb = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		lineRenderer = GetComponentInChildren<LineRenderer>();


	}

    private void Update()
    {
		lineRenderer.positionCount = 0;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, hookLineOriginPosition);
        lineRenderer.SetPosition(1, transform.position + hookLineConnectPositionOffset);
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

		if(isMoving)
		{
			if(Input.GetKey(KeyCode.F))
			{
				Move(-horizontalMoveSpeed);
			}

			else if(Input.GetKey(KeyCode.J))
			{
				Move(horizontalMoveSpeed);
			}
		}
	}

    void Move(float moveSpeed)
    {
		transform.position += new Vector3(moveSpeed, 0f, 0f);
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, horizontalRangeClamp.x, horizontalRangeClamp.y), transform.position.y, 0f);
	}
	void Toggle(bool isOn)
	{
		spriteRenderer.enabled = isOn;
		isMoving = isOn;
	}

	void ToggleLogic(bool isOn)
	{
        circleCollider.enabled = isOn;
        rb.simulated = isOn;
		isMoving = isOn;
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
