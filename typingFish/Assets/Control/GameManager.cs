using System;
using System.Collections.Generic;
using NUnit.Framework;
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
    public void SetState(GameState state)
    { 
        curState = state;
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
        SetState(GameState.FISHING);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
