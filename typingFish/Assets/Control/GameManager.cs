using System;
using UnityEngine;

[Flags]
public enum GameState
{
    NULL = 0,
    FISHING = 1,
    TYPING = 2,
    PAUSED = 4,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    GameState curState = GameState.NULL;
    public GameState GetState(){ return curState; }
    public void SetState(GameState state){ curState = state; }

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
