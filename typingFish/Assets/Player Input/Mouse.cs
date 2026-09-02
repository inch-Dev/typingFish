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
                break;
            case MouseState.MOUSE:
                Cursor.visible = true;
                break;
        }

    }
    public void HandleState()
    {
        switch (GameManager.instance.GetState())
        {
            case GameState.FISHING:
                SetState(MouseState.HOOK);
                break;
            case GameState.CASTING:
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
}
