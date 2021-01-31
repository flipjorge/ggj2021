using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GameState { NullState, Intro, Menu, InGame, GameOver }

public enum ScoreValue
{
    DeliveredOwner, DeliveredBalcony, Lost
}
public class GameManager : Singleton<GameManager>
{
    #region Flow
    private GameState currentGameState;

    public UnityEvent<GameState> OnGameStateChanged;

    public void SetGameState(GameState gameState)
    {
        this.currentGameState = gameState;
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged?.Invoke(gameState);
        }

        if (currentGameState == GameState.InGame)
        {
            SceneManager.LoadScene(2);
        }
        else if (currentGameState != GameState.Intro)
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion

    #region Gameplay
    public int Score { get; private set; }
    private int ZeroScoreTries = 0;
    private readonly int ZeroScoreMaxTries = 3;

    private TimeSpan SessionTime { get; set; }
    private DateTime SessionTimeStarted { get; set; }

    public TimeSpan TimeLeft { get
        {
            return SessionTime - (DateTime.UtcNow - SessionTimeStarted);
        } 
    }

    public int SessionTimeInSeconds;

    public UnityEvent OnGameplayStarted;
    public UnityEvent OnGameplayEnded;
    public UnityEvent<int> OnScoreChanged;

    private bool gameplayActive = false;

    public void ActivateGameplay()
    {
        SessionTime = TimeSpan.FromSeconds(SessionTimeInSeconds);
        SessionTimeStarted = DateTime.UtcNow;
        Score = 0;
        ZeroScoreTries = 0;
        OnScoreChanged?.Invoke(Score);
        OnGameplayStarted?.Invoke();
        gameplayActive = true;
    }

    public void ResetGameplay()
    {
        Score = 0;
        ZeroScoreTries = 0;
        OnScoreChanged?.Invoke(Score);
    }

    public void ChangeScore(ScoreValue scoreValue)
    {
        int scoreChange = scoreValue switch
        {
            ScoreValue.DeliveredOwner => 2,
            ScoreValue.DeliveredBalcony => 1,
            ScoreValue.Lost => -2,
            _ => 0,
        };
        Score += scoreChange;
        if (Score <= 0)
        {
            ZeroScoreTries++;
            Score = 0;
            print("Zero Score Tries: (" + ZeroScoreTries + "/" + ZeroScoreMaxTries + ")");
        }
        print("Score: " + Score);

        if(gameplayActive)
            OnScoreChanged?.Invoke(Score);
    }
    #endregion

    #region Unity Events
    void Start()
    {
        Initialize();
    }
    #endregion

    void Update()
    {
        if (gameplayActive)
        {
            if(TimeLeft.TotalSeconds <= 0)
            {
                gameplayActive = false;
                OnGameplayEnded?.Invoke();
            }
        }
    }

    #region Initialization
    public void Initialize()
    {
        GameManager.Instance.SetGameState(GameState.Intro);
    }
    #endregion

    

    #region Events
    public void RegisterOnGameplayStartEvent(UnityAction callback)
    {
        OnGameplayStarted.AddListener(callback);
    }

    public void UnRegisterOnGameplayStartEvent(UnityAction callback)
    {
        OnGameplayStarted.RemoveListener(callback);
    }

    public void RegisterOnGameplayEndingEvent(UnityAction callback)
    {
        OnGameplayEnded.AddListener(callback);
    }

    public void UnRegisterOnGameplayEndingEvent(UnityAction callback)
    {
        OnGameplayEnded.RemoveListener(callback);
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
