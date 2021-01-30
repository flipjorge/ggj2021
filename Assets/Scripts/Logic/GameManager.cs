using UnityEngine.Events;

public enum GameState { NullState, Intro, Menu, InGame, GameOver }

public class GameManager : Singleton<GameManager>
{
    private GameState currentGameState;
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
    }

    
}
