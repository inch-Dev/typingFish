using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class UIManager : MonoBehaviour, IStateable
{
    public void HandleState() 
    {
        SetUI(GameManager.instance.GetState());
    }
    public static UIManager instance;
    List<UI> uis;
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

        uis = new List<UI>(GameObject.FindObjectsByType<UI>(FindObjectsInactive.Exclude, FindObjectsSortMode.None));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
