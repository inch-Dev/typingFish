using UnityEngine;

public class SessionManager : MonoBehaviour, IStateable
{
    public void HandleState()
    {
        switch (GameManager.instance.GetState())
        {
            case GameState.CASTING:
            case GameState.PAUSED:
                isCountingTimer = false;
                break;
            case GameState.FISHING:
            case GameState.TYPING:
                isCountingTimer = true;
                break;
        }
    }



    public static SessionManager instance;
    [SerializeField] float sessionTimeSeconds;
    float sessionSecondsElapsed;
    bool isCountingTimer = true;
    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void FixedUpdate()
    {
        if(isCountingTimer)
        {
            sessionSecondsElapsed += Time.fixedDeltaTime;
            SessionUI.instance.DisplayTime(sessionTimeSeconds - sessionSecondsElapsed);

            if (sessionSecondsElapsed >= sessionTimeSeconds)
                EndSession();
        }
    }

    void EndSession()
    {
        Debug.Log("Session is over!");
        GameManager.instance.SetState(GameState.SESSION_OVER);
    }
}
