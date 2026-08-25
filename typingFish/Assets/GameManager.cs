using UnityEngine;


public enum GameState
{
    NULL = -1,
    FISHING,
    TYPING,
    PAUSED
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameState curState = GameState.NULL;
    public GameState GetState(){ return curState; }
    public void SetState(GameState state){ curState = state; }

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
