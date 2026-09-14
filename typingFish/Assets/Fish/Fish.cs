using Unity.VisualScripting;
using UnityEditor.Rendering;
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

    [SerializeField] float ascendSpeed;
    [SerializeField] Vector2 moveSpeedRange;
    public float moveSpeed;
    public Vector2 moveDirection;
    float fishingResetYPosition;
    bool canMove = true;


    public void SetMove(bool move) { canMove = move; }
	public void HandleState()
	{
        switch (GameManager.instance.GetState())
        {
            case GameState.TYPING:
                collider.enabled = false;
                canMove = true;
                break;
            case GameState.CASTING:
                collider.enabled = false;
                canMove = true;
                break;
            case GameState.FISHING:
                collider.enabled = true;
                canMove = true;
                //transform.position = new Vector3(transform.position.x, fishingResetYPosition, 0f);
                break;
            case GameState.PAUSED:
            case GameState.SESSION_OVER:
                canMove = false;
                break;

        }
    }

    private void Start()
    {
        collider = GetComponentInChildren<CapsuleCollider2D>();
        moveSpeed = Random.Range(moveSpeedRange.x, moveSpeedRange.y);
        fishingResetYPosition = transform.position.y;
    }

    private void FixedUpdate()
    {
        if(canMove)
            Move();
    }

    void Move()
    {
        Vector3 acceleration = new Vector3(moveSpeed * moveDirection.x * Time.fixedDeltaTime, ascendSpeed * Time.fixedDeltaTime);
        transform.position += acceleration;
    }

}
