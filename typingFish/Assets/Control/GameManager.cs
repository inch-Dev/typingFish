using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Flags]
public enum GameState
{
    NULL = 0,
    CASTING = 1,
    FISHING = 2,
    TYPING = 4,
    PAUSED = 6,
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameState curState = GameState.NULL;

    List<IStateable> stateables = new List<IStateable>();

    void SetStateables()
    {
        stateables.Clear();

        MonoBehaviour[] allScripts = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach(MonoBehaviour script in allScripts)
        {
            if (script is IStateable)
                stateables.Add(script as IStateable);
        }
    }

    public GameState GetState(){ return curState; }

    public void SetState(int state)
    {
        SetState((GameState)state);
    }

    public void SetState(GameState state)
    {
        SetStateables();

        curState = state;
        Debug.Log($"Setting state to {curState}");
        foreach(IStateable stateable in stateables)
        {
            stateable.HandleState();
        }

    }

    void Start()
    {
        if (instance == null)
            instance = this;

        SetStateables();
        SetState(GameState.CASTING);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
