using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameState { NullState, Intro, MainMenu, InGame }

public class GameManager : Singleton<GameManager>
{
    private GameState currentGameState;
    public UnityEvent OnGameStateChanged;

    public ObjectFinderService ofs;

    public int SessionTimeInSeconds;

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        ofs = new ObjectFinderService();
        currentGameState = GameState.Intro;
    }

    public void SetGameState(GameState gameState)
    {
        this.currentGameState = gameState;
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged?.Invoke();
        }
    }

    
}
