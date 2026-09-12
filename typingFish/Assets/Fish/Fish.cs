using Unity.VisualScripting;
using UnityEditor.Presets;
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
    bool canMove = true;
    float resetYPosition;

    public void SetMove(bool move) { canMove = move; }
	public void HandleState()
	{
        switch (GameManager.instance.GetState())
        {
            case GameState.TYPING:
                collider.enabled = false;
                canMove = true;
                break;
            case GameState.FISHING:
                collider.enabled = true;
                canMove = true;
                //Reset position
                transform.position = new Vector3(transform.position.x, resetYPosition, 0f);
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
        resetYPosition = transform.position.y;
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
