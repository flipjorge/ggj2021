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


}
