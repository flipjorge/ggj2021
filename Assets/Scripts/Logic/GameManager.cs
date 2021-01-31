using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState { NullState, Intro, Menu, InGame, GameOver }

public class GameManager : Singleton<GameManager>
{
    private GameState currentGameState;
    //private GameState previousGameState;
    public UnityEvent<GameState> OnGameStateChanged;

    //public ObjectFinderService ofs;

    public int SessionTimeInSeconds;

    #region Gameplay
    public int Score { get; private set; }

    public TimeSpan TimeLeft { get; private set; }

    public int ObjectsDelivered { get; private set; }

    public int ObjectsNotDelivered { get; private set; }

    public UnityEvent OnGameplayStarted;
    public UnityEvent<int> OnScoreChanged;
    #endregion

    void Start()
    {
        GameManager.Instance.Initialize();
    }

    public void Initialize()
    {
        //ofs = new ObjectFinderService();
        GameManager.Instance.SetGameState(GameState.Intro);
    }

    public void SetGameState(GameState gameState)
    {
        this.currentGameState = gameState;
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged?.Invoke(gameState);
        }
        
        if(currentGameState == GameState.InGame)
        {
            SceneManager.LoadScene(2);
            Time.timeScale = 0;
        }
        else if(currentGameState != GameState.Intro)
        {
            SceneManager.LoadScene(1);
        }
    }

    #region Gameplay
    public void ActivateGameplay()
    {
        Time.timeScale = 1;
        Score = 0;
        OnScoreChanged?.Invoke(Score);
        OnGameplayStarted?.Invoke();
    }

    public void Scored(int value = 1)
    {
        Score += value;
        OnScoreChanged?.Invoke(Score);
    }

    public void RegisterOnStartEvent(UnityAction callback)
    {
        OnGameplayStarted.AddListener(callback);
    }

    public void UnRegisterOnStartEvent(UnityAction callback)
    {
        OnGameplayStarted.RemoveListener(callback);
    }

    public void RegisterOnScoreEvent(UnityAction<int> callback)
    {
        OnScoreChanged.AddListener(callback);
    }

    public void UnRegisterOnScoreEvent(UnityAction<int> callback)
    {
        OnScoreChanged.RemoveListener(callback);
    }
    #endregion

}
