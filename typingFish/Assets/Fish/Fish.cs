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
    public float moveSpeed;
    public Vector2 moveDirection;
    bool canMove = true;


    public void SetMove(bool move) { canMove = move; }
	public void HandleState()
	{
        switch (GameManager.instance.GetState())
        {
            case GameState.TYPING:
                //Debug.Log("Toggle off simulation");
                collider.enabled = false;
                canMove = true;
                break;
            case GameState.FISHING:
                collider.enabled = true;
                canMove = true;
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
