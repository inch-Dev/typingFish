using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public struct UI
{
    public string name;
    public CanvasGroup canvasGroup;
    public GameState activeStates;

    public void Toggle(bool isActive)
    {
        canvasGroup.enabled = isActive;
        canvasGroup.blocksRaycasts = isActive;
        canvasGroup.alpha = isActive ? 1 : 0;
    }

    public void Toggle(GameState state)
    {
        if(!activeStates.HasFlag(state))
            Toggle(false);

        else 
            Toggle(true);
    }

    public void Toggle(GameState state, bool isActive)
    {
        if (!activeStates.HasFlag(state))
            return;
        Toggle(isActive);
    }
}



public class UIManager : MonoBehaviour, IStateable
{
    public void HandleState() 
    {
        SetUI(GameManager.instance.GetState());
    }
    public static UIManager instance;
    [SerializeField] List<UI> uis;
    public void SetUI(GameState activeState)
    {
        foreach(UI ui in uis)
        {
            ui.Toggle(activeState);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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
