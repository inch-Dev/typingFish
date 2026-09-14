using UnityEngine;

public class SessionManager : MonoBehaviour, IStateable
{
    public void HandleState()
    {
        switch (GameManager.instance.GetState())
        {
            case GameState.START:
            case GameState.PAUSED:
            case GameState.SESSION_OVER:
                isCountingTimer = false;
                break;
            case GameState.FISHING:
            case GameState.TYPING:
            case GameState.CASTING:
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
        SessionUI.instance.DisplayTime(sessionTimeSeconds);
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
