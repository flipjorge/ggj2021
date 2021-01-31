using UnityEngine;

[CreateAssetMenu(fileName = "GameStatePrefabs", menuName = "ScriptableObjects/GameState", order = 1)]
public class GameStatePrefabs : ScriptableObject
{
    [SerializeField]
    private GameObject intro;

    [SerializeField]
    private GameObject menu;

    [SerializeField]
    private GameObject inGame;

    [SerializeField]
    private GameObject gameOver;

    public GameObject GetPrefab(GameState state)
    {
        switch (state)
        {
            case GameState.Intro: return intro;
            case GameState.Menu: return menu;
            case GameState.InGame: return inGame;
            case GameState.GameOver: return gameOver;
        }

        return null;
    }
}
