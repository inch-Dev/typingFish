using UnityEngine;

public enum MouseState
{
    NULL = -1,
    MOUSE,
    HOOK,
    NUM_STATES
}



public class Mouse : MonoBehaviour, IStateable
{
    public static Mouse instance;
    MouseState state;
    public void SetState(MouseState newState)
    {
        state = newState;

        switch (state)
        {
            case MouseState.HOOK:
                Cursor.visible = false;
                hook.SetActive(true);
                break;
            case MouseState.MOUSE:
                Cursor.visible = true;
                hook.SetActive(false);
                break;
        }

    }

    [Header("Hook State")]
    [SerializeField] GameObject hook;
    [SerializeField] float hookMoveSpeed;
    public void HandleState()
    {
        switch (GameManager.instance.GetState())
        {
            case GameState.CASTING:
            case GameState.FISHING:
                SetState(MouseState.HOOK);
                break;

            case GameState.TYPING:
                SetState(MouseState.MOUSE);
                break;
        }

    }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == MouseState.HOOK)
            HookFollowMouse();
    }

    void HookFollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        transform.position = Vector2.Lerp(transform.position, mousePosition, hookMoveSpeed);
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInParent<Fish>())
        {
            GameManager.instance.SetState(GameState.TYPING);
            FishManager.instance.SetCatchingFish(collision.gameObject.GetComponentInParent<Fish>());
        }
	}
}
