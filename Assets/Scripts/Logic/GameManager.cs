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
    private GameState currentGameState;
    //private GameState previousGameState;
    public UnityEvent<GameState> OnGameStateChanged;

    private int Score = 0;
    private int ZeroScoreTries = 0;
    private readonly int ZeroScoreMaxTries = 3;

    //public ObjectFinderService ofs;

    public int SessionTimeInSeconds;

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
            //Time.timeScale = 0;
        }
        else if(currentGameState != GameState.Intro)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void ActivateGameplay()
    {
        Time.timeScale = 1;
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
        if(Score <= 0)
        {
            ZeroScoreTries++;
            Score = 0;
            print("Zero Score Tries: (" + ZeroScoreTries + "/" + ZeroScoreMaxTries + ")");
        }
        print("Score: " + Score);
    }
}
